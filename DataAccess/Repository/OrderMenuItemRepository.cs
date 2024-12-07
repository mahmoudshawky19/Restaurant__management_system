using Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class OrderMenuItemRepository : Repository<OrderMenuItem>, IOrderMenuItemRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderMenuItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {this.dbContext = dbContext;
        }
    }
}
