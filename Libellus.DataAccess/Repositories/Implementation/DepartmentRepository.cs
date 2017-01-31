using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IObjectContextAdapter dbContext) : base(dbContext)
        {
        }
    }
}
