﻿using Libellus.BusinessCore.Processors.Interface;
using Libellus.CommonConcerns.Constants;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libellus.BusinessCore.Processors.Implementation
{
    public class ProjectProcessor : BaseProcessor, IProjectProcessor
    {
        public ProjectProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Subscribe(Project project)
        {
            UnitOfWork.ProjectRepository.Attach(project);
            //UnitOfWork.ProjectRepository.Update(project);
            UnitOfWork.Save();
        }

        public void UnSubscribe(Project project)
        {
            UnitOfWork.ProjectRepository.Detach(project);
            //UnitOfWork.ProjectRepository.Update(project);
            UnitOfWork.Save();
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return UnitOfWork.ProjectRepository.GetAll();
        }

        public void CreateNewProject(Project model)
        {
            UnitOfWork.ProjectRepository.Add(model);
            //UnitOfWork.ProjectRepository.Save();
            UnitOfWork.Save();
        }

        public IEnumerable<Project> GetAllProjectsInDepartment(int departmentId)
        {
            var projects = UnitOfWork.ProjectRepository.GetAll().Where(x => x.DepartmentId == departmentId);
            return projects.ToList();
        }

        public IEnumerable<Project> GetAllWaitingApproval()
        {
            return UnitOfWork.ProjectRepository.GetAll().Where(x => x.Status == CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.New));
        }

        public IEnumerable<Project> GetAllInProgress()
        {
            return UnitOfWork.ProjectRepository.GetAll().Where(x => x.Status == CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.InProgress));
        }
    }
}
