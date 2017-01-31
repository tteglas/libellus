using Libellus.BusinessCore.Processors.Interface;
using Libellus.DataAccess.UoW;

namespace Libellus.BusinessCore.Processors.Implementation
{
    public class BaseProcessor : IBaseProcessor
    {
        protected IUnitOfWork UnitOfWork;

        public BaseProcessor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
