using PointOfSaleApp.Domain.Repositories;
using System;

namespace PointOfSaleApp.Domain.Factories
{
    public static class RepositoryFactory
    {
        public static TRepository GetRepository<TRepository>() where TRepository : BaseRepository
        {
            var context = DbContextFactory.GetPointOfSaleDbContext();
            return (TRepository)Activator.CreateInstance(typeof(TRepository), context);
        }  
        
        public static TRepository GetRepository<TRepository>(BaseRepository parentRepository) where TRepository : BaseRepository
        {
            var context = DbContextFactory.GetPointOfSaleDbContext();
            return (TRepository)Activator.CreateInstance(typeof(TRepository), context, parentRepository);
        }
    }
}
