using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libellus.DataAccess.Domain
{
    public class UserProjectDepartment
    {
        public int ProjectId { get; set; }
        public int DepartmentId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Department Department { get; set; }
    }
}
