using Authentication.Managers;
using Libellus.BusinessCore.Processors.Interface;
using Libellus.CommonConcerns.Constants;
using Libellus.DataAccess.Domain;
using Libellus.Models;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libellus.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private IProjectProcessor _projectProcessor;

        public ProjectController(IProjectProcessor projectProcessor)
        {
            _projectProcessor = projectProcessor;
        }

        public UserCustomManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserCustomManager>();
            }
        }

        public ActionResult Index(int? id)
        {
            var model = new ProjectViewModel();
            var user = UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            // todo: temp code here
            int departmentId = 1;
            if (user.Result != null)
            {
                departmentId = user.Result.Department == null ? 1 : user.Result.Department.Id;
            }
            var availableProjects = _projectProcessor.GetAllProjectsInDepartment(departmentId);

            var availableProfessors = UserManager.Users
                .Where(x => x.FacultyRole.Id == (int)CommonHelper.FacultyRole.Professor)
                .Where(x => x.Department.Id == departmentId).ToList();

            //todo: this should be take into a different controller
            var pageSize = id == null ? 5 : id.Value;
            var pagedList = new PagedList<Project>(availableProjects, 1, pageSize);

            model.AvailableProfessors = availableProfessors;
            model.AvailableProjects = pagedList;
            model.User = user.Result;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProject(ProjectViewModel model)
        {
            var user = UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            var proj = new Project();
            proj.CreatedDate = DateTime.Now;
            proj.ModifiedDate = DateTime.Now;
            proj.Status = CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.New);
            proj.Name = model.Name;
            proj.Progress = 0;
            proj.Description = model.Description;
            proj.AddedBy = user.Result.UserName;
            //proj.Department.Id = user.Result.Department.Id;
            //proj.UserId = user.Result.Id;
            //user.Result.Projects.Add(proj);

            _projectProcessor.CreateNewProject(proj);
            return RedirectToAction("Index", "Project");
        }

        #region Partial views

        public ActionResult GetProfessorProjects(int? page, bool requestedApproval)
        {
            var model = new ProjectViewModel();
            var pageNumber = page ?? 1;
            var pageSize = 5;
            PagedList<Project> pagedList;
            var viewToReturn = string.Empty;

            ViewBag.RequestedApproval = requestedApproval;

            if (requestedApproval)
            {
                var waitingApproval = _projectProcessor.GetAllWaitingApproval();
                pagedList = new PagedList<Project>(waitingApproval, pageNumber, pageSize);
                viewToReturn = "ProfWaitingApprovalView";
            }
            else
            {
                var inProgress = _projectProcessor.GetAllInProgress();
                pagedList = new PagedList<Project>(inProgress, pageNumber, pageSize);
                viewToReturn = "ProfInProgressView";
            }

            model.AvailableProjects = pagedList;
            model.ProcessedDataOnModel = true;
            return View(viewToReturn, model);
        }

        public ActionResult ViewProject(int id)
        {
            var user = UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var projectToView = _projectProcessor.GetAllProjectsInDepartment(user.Result.Department.Id).FirstOrDefault(x => x.Id == id);

            var model = new ProjectViewModel();
            model.Name = projectToView.Name;
            model.Description = projectToView.Description;
            model.ProcessedDataOnModel = true;

            return View("ProjectDetailsView", model);
        }

        public PartialViewResult PagedListResult(int? page)
        {
            var user = UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            var availableProjects = _projectProcessor.GetAllProjectsInDepartment(user.Result.Department.Id);

            var pageNumber = page ?? 1;
            var pageSize = 5;
            var pagedList = new PagedList<Project>(availableProjects, pageNumber, pageSize);

            var partialToRender = "_ProfessorProjectPartial";
            var model = new ProjectViewModel();
            model.AvailableProjects = pagedList;
            //model.User = user.Result; todo: look this up

            return PartialView(partialToRender, model);
        }

        public ActionResult AddProjectView()
        {
            var user = UserManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var model = new ProjectViewModel();
            model.User = user.Result;
            return View("AddProjectView", model);
        }

        #endregion
    }
}
