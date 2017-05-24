using Libellus.DataAccess.Domain;

namespace Libellus.BusinessCore.Processors.Interface
{
    public interface ISubscriptionProcessor : IBaseProcessor
    {
        void CreateSubscription(Subscription subscription);

        void RemoveSubscription(Subscription subscription);
    }
}