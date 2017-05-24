using Libellus.DataAccess.Database;
using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibellusDbContext _dbContext;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public UnitOfWork(LibellusDbContext dbContext,
            IFacultyRepository facultyRepository,
            IDepartmentRepository departmentRepository,
            IProjectRepository projectRepository,
            ITaskRepository taskRepository,
            ISubscriptionRepository subscriptionRepository)
        {
            _dbContext = dbContext;

            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IFacultyRepository FacultyRepository => _facultyRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IProjectRepository ProjectRepository => _projectRepository;

        public ITaskRepository TaskRepository => _taskRepository;

        public ISubscriptionRepository SubscriptionRepository => _subscriptionRepository;
    }
}
