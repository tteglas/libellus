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
using Libellus.Managers;

namespace Libellus.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        #region Private Members
        private readonly IProjectProcessor _projectProcessor;
        private readonly UserCustomManager _userManager;
        #endregion

        #region Constructor
        public ProjectController(IProjectProcessor projectProcessor,
            UserCustomManager userManager)
        {
            _projectProcessor = projectProcessor;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public ActionResult Index(int? id)
        {
            var model = new ProjectViewModel();
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            var allDepartmentProjects = _projectProcessor.GetAllProjectsInDepartment(user.Result.Department.Id).ToList();
            var allInProgressProject =
                allDepartmentProjects.Where(
                    x => x.Status == CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.InProgress));

            var departmentProfessors = _userManager.Users
                .Where(x => x.FacultyRole.Id == (int)CommonHelper.FacultyRole.Professor)
                .Where(x => x.Department.Id == user.Result.Department.Id).ToList();

            //todo: this should be take into a different method ?
            var pageSize = id ?? 5;

            model.AvailableProfessors = departmentProfessors;
            model.AvailableProjects = new PagedList<Project>(allDepartmentProjects, 1, pageSize);
            model.InProgressProjects = new PagedList<Project>(allInProgressProject, 1, pageSize);
            model.User = user.Result;
            
            // View to return - based on Faculty Role ( student / professor)
            if (user.Result.FacultyRole.Id == (int) CommonHelper.FacultyRole.Professor)
            {
                return View("ProfessorIndex", model);
            }
            if(user.Result.FacultyRole.Id == (int)CommonHelper.FacultyRole.Student)
            {
                return View("StudentIndex", model);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProject(ProjectViewModel model)
        {
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var proj = new Project();
            proj.CreatedDate = DateTime.Now;
            proj.ModifiedDate = DateTime.Now;
            proj.Status = CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.New);
            proj.Name = model.Name;
            proj.Progress = 0;
            proj.Description = model.Description;
            proj.AddedBy = user.Result.UserName;
            proj.DepartmentId = user.Result.Department.Id;
            proj.UserId = user.Result.Id;

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
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            //var projectToView = _projectProcessor.GetAllProjectsInDepartment(user.Result.Department.Id).FirstOrDefault(x => x.Id == id);
            //var projectToView = user.Result.Projects.FirstOrDefault(x => x.Id == id);
            var model = new ProjectViewModel();
            //model.Name = projectToView.Name;
            //model.Description = projectToView.Description;
            model.ProcessedDataOnModel = true;

            return View("ProjectDetailsView", model);
        }

        public PartialViewResult PagedListResult(int? page)
        {
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);

            //var availableProjects = _projectProcessor.GetAllProjectsInDepartment(user.Result.Department.Id);

            var pageNumber = page ?? 1;
            var pageSize = 5;
            var pagedList = new PagedList<Project>(new System.Collections.Generic.List<Project>(), pageNumber, pageSize);

            var partialToRender = "_ProfessorProjectPartial";
            var model = new ProjectViewModel();
            model.AvailableProjects = pagedList;
            //model.User = user.Result; todo: look this up

            return PartialView(partialToRender, model);
        }

        public ActionResult AddProjectView()
        {
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var model = new ProjectViewModel();
            model.User = user.Result;
            return View("AddProjectView", model);
        }

        #endregion

        #endregion
    }
}
