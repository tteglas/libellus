using Libellus.BusinessCore.Processors.Interface;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.UoW;

namespace Libellus.BusinessCore.Processors.Implementation
{
    public class SubscriptionProcessor : BaseProcessor, ISubscriptionProcessor
    {
        public SubscriptionProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateSubscription(Subscription subscription)
        {
            UnitOfWork.SubscriptionRepository.Add(subscription);
            UnitOfWork.Save();
        }

        public void RemoveSubscription(Subscription subscription)
        {
            UnitOfWork.SubscriptionRepository.Delete(subscription);
            UnitOfWork.Save();
        }
    }
}