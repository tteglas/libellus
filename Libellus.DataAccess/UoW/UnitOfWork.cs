using Libellus.DataAccess.Database;
using Libellus.DataAccess.Repositories.Interface;
using System.Data.Entity.Infrastructure;

namespace Libellus.DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibellusDbContext _dbContext;
        private IFacultyRepository _facultyRepository;
        private IDepartmentRepository _departmentRepository;
        private IProjectRepository _projectRepository;
        private ITaskRepository _taskRepository;

        public UnitOfWork(LibellusDbContext dbContext,
            IFacultyRepository facultyRepository,
            IDepartmentRepository departmentRepository,
            IProjectRepository projectRepository,
            ITaskRepository taskRepository)
        {
            _dbContext = dbContext;

            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IFacultyRepository FacultyRepository { get { return _facultyRepository; } }

        public IDepartmentRepository DepartmentRepository { get { return _departmentRepository; } }

        public IProjectRepository ProjectRepository { get { return _projectRepository; } }

        public ITaskRepository TaskRepository { get { return _taskRepository; } }
    }
}
