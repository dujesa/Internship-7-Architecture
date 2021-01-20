using PointOfSaleApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Domain.Enums;

namespace PointOfSaleApp.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly PointOfSaleDbContext DbContext;

        public BaseRepository(PointOfSaleDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ResponseResultType SaveChanges()
        {
            var hasChanges = DbContext.SaveChanges() > 0;

            return hasChanges ? ResponseResultType.Success : ResponseResultType.NoChanges;
        }
    }
}
