using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(IObjectContextAdapter dbContext) : base(dbContext)
        {
        }
    }
}
