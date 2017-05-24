using Libellus.DataAccess.Database;
using Libellus.DataAccess.Domain;
using Libellus.DataAccess.Repositories.Interface;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(LibellusDbContext dbContext) : base(dbContext)
        {
        }
    }
}