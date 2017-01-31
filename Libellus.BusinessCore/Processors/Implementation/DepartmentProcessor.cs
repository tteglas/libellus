using Libellus.BusinessCore.Processors.Interface;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.UoW;
using System.Collections.Generic;

namespace Libellus.BusinessCore.Processors.Implementation
{
    public class DepartmentProcessor : BaseProcessor, IDepartmentProcessor
    {
        public DepartmentProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Department> GetAllDepartments()
        {
            return UnitOfWork.DepartmentRepository.GetAll();
        }
    }
}
