using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(IObjectContextAdapter dbContext) : base(dbContext)
        {
        }
    }
}
