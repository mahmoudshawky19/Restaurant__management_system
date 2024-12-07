using Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class SubmitReviewRepository : Repository<SubmitReview>, ISubmitReviewRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SubmitReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
