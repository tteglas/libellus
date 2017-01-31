using Libellus.BusinessCore.Processors.Interface;
using Libellus.DataAccess.UoW;
using Libellus.DataAccess.Domain;
using System.Collections.Generic;

namespace Libellus.BusinessCore.Processors.Implementation
{
    public class FacultyProcessor : BaseProcessor, IFacultyProcessor
    {
        public FacultyProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Faculty> GetAllFaculties()
        {
            return UnitOfWork.FacultyRepository.GetAll();
        }

        public List<FacultyRole> GetAllFacultyRoles()
        {
            return UnitOfWork.FacultyRoleRepository.GetAll();
        }
    }
}
