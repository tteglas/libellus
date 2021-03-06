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

        public void CreateNewProject(Project model)
        {
            UnitOfWork.ProjectRepository.Add(model);
            UnitOfWork.ProjectRepository.Save();
            //UnitOfWork.Save();
        }

        public IEnumerable<Project> GetAllProjectsInDepartment(int departmentId)
        {
            return UnitOfWork.ProjectRepository.GetAll().Where(x => x.Department.Id == departmentId);
        }

        public IEnumerable<Project> GetAllWaitingApproval()
        {
            return UnitOfWork.ProjectRepository.GetAll().Where(x => x.Status == CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.WaitingApproval));
        }

        public IEnumerable<Project> GetAllInProgress()
        {
            return UnitOfWork.ProjectRepository.GetAll().Where(x => x.Status == CommonHelper.ReadValueForProjectStatus(CommonHelper.ProjectStatus.InProgress));
        }
    }
}
