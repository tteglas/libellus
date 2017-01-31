using Libellus.DataAccess.Domain;
using System.Collections.Generic;

namespace Libellus.BusinessCore.Processors.Interface
{
    public interface IDepartmentProcessor
    {
        List<Department> GetAllDepartments();
    }
}
