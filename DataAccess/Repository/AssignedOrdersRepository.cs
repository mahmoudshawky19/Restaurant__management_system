
 using  Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class AssignedOrdersRepository : Repository<AssignedOrders>, IAssignedOrdersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AssignedOrdersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
