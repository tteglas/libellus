using Libellus.DataAccess.Domain;
using System.Collections.Generic;

namespace Libellus.BusinessCore.Processors.Interface
{
    public interface IProjectProcessor : IBaseProcessor
    {
        void CreateNewProject(Project model);

        //IEnumerable<Project> GetAllProjectsInDepartment(string user);
        IEnumerable<Project> GetAllProjectsInDepartment(int departmentId);

        IEnumerable<Project> GetAllWaitingApproval();

        IEnumerable<Project> GetAllInProgress();
    }
}
