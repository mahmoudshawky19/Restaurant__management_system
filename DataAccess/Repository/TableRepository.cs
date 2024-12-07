using Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class TableRepository : Repository<RestaurantTable>, ITableRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {this.dbContext = dbContext;
        }
    }
}
