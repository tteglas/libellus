using System.Data.Entity.Infrastructure;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;
using Libellus.DataAccess.Database;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(LibellusDbContext dbContext) : base(dbContext)
        {
        }
    }
}
