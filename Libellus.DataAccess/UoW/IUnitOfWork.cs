using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.UoW
{
    public interface IUnitOfWork
    {
        void Save();

        IFacultyRepository FacultyRepository { get; }

        IDepartmentRepository DepartmentRepository { get; }

        IProjectRepository ProjectRepository { get; }

        ITaskRepository TaskRepository { get; }

        ISubscriptionRepository SubscriptionRepository { get; }
    }
}
