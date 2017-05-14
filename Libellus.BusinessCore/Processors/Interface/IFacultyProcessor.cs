using Libellus.DataAccess.Domain;
using System.Collections.Generic;

namespace Libellus.BusinessCore.Processors.Interface
{
    public interface IFacultyProcessor : IBaseProcessor
    {
        List<Faculty> GetAllFaculties();
        //List<FacultyRole> GetAllFacultyRoles();
    }
}
