using Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class OrderTableRepository : Repository<OrderTable>, IOrderTableRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderTableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
