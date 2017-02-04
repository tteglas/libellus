using Libellus.DataAccess.Domain;
using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Repositories.Interface;
using Libellus.DataAccess.Database;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(LibellusDbContext dbContext) : base(dbContext)
        {
        }
    }
}
