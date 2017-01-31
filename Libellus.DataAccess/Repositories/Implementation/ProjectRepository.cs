using Libellus.DataAccess.Domain;
using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IObjectContextAdapter dbContext) : base(dbContext)
        {
        }
    }
}
