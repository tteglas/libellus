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
using System.Web.Security;
using System.Web.UI;
using Libellus.DataAccess.UoW;
using Libellus.Managers;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var userProjects = allDepartmentProjects.Where(x => x.User.Id == user.Result.Id).ToList();
            var userInProgressProjects = allDepartmentProjects.Where(x => x.Status == CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.InProgress)).Where(x => x.User.Id == user.Result.Id);

            var departmentProfessors = _userManager.Users
                .Where(x => x.Roles.FirstOrDefault(role => role.RoleId == CommonHelper.ROLE_Professor).RoleId == CommonHelper.ROLE_Professor)
                .Where(x => x.Department.Id == user.Result.Department.Id).ToList();

            //todo: this should be take into a different method ?
            var pageNumber = id ?? 1;

            model.AvailableProfessors = departmentProfessors;
            model.AllProjects = new PagedList<Project>(allDepartmentProjects, pageNumber, 5);
            model.UserProjects = new PagedList<Project>(userProjects, pageNumber, 5);
            model.UserInProgressProjects = new PagedList<Project>(userInProgressProjects, pageNumber, 5);
            model.User = user.Result;
            
            // View to return - based on Faculty Role ( student / professor)
            if (User.IsInRole(CommonHelper.ROLE_Professor))
            {
                return View("ProfessorIndex", model);
            }
            if (User.IsInRole(CommonHelper.ROLE_Student))
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

            user.Result.Projects.Add(proj);
            _projectProcessor.CreateNewProject(proj);
            return RedirectToAction("Index", "Project");
        }

        public ActionResult ViewProject(int id)
        {
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var projectToView = _projectProcessor.GetAllProjectsInDepartment(user.Result.Department.Id).FirstOrDefault(x => x.Id == id);
            if (projectToView == null)
                return View("Error");
            
            var model = new ProjectViewModel();
            model.Id = projectToView.Id;
            model.Name = projectToView.Name;
            model.Description = projectToView.Description;

            return View("ProjectDetailsView", model);
        }

        public ActionResult Subscribe(int id)
        {
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var project = _projectProcessor.GetAllProjects().FirstOrDefault(x => x.Id == id);

            if (project != null && user.Result != null)
            {
                
            }
            return View("");
        }

        #region Partial views

        public ActionResult GetPaginatedProjects(int? page)
        {
            var user = _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var pageNumber = page ?? 1;
            const int pageSize = 5;

            var allProjects = _projectProcessor.GetAllProjectsInDepartment(user.Result.DepartmentId);
            var pagedList = new PagedList<Project>(allProjects, pageNumber, pageSize);

            return View("_PaginatedProjects", pagedList);
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
            model.AllProjects = pagedList;
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
