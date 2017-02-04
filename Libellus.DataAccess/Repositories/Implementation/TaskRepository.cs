using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;
using Libellus.DataAccess.Database;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(LibellusDbContext dbContext) : base(dbContext)
        {
        }
    }
}
