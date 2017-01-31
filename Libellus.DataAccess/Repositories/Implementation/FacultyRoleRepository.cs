using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class FacultyRoleRepository : BaseRepository<FacultyRole>, IFacultyRoleRepository
    {
        public FacultyRoleRepository(IObjectContextAdapter dbContext) : base(dbContext)
        {
        }
    }
}
