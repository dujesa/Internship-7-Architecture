using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Actions.InventoryActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Factories
{
    class InventoryActionsFactory
    {
        public static InventoryParentActions GetInventoryParentActions()
        {
            var actions = new List<IAction>
            {
                new InventoryAddAction(RepositoryFactory.GetRepository<OfferRepository>()),
                //new InventoryDeleteAction(RepositoryFactory.GetRepository<OfferCategoryRepository>()),
                //new InventoryReviewAction(RepositoryFactory.GetRepository<OfferCategoryRepository>(), RepositoryFactory.GetRepository<OfferRepository>()),
                //new InventoryQuantityManagementAction(RepositoryFactory.GetRepository<OfferCategoryRepository>(), RepositoryFactory.GetRepository<OfferRepository>()),
                //new ArticlesInventoryAction(RepositoryFactory.GetRepository<OfferCategoryRepository>(), RepositoryFactory.GetRepository<OfferRepository>()),
                //new AvailableSubscriptionsInventoryAction(RepositoryFactory.GetRepository<OfferCategoryRepository>(), RepositoryFactory.GetRepository<OfferRepository>()),
                //new AvailableServicesInventoryAction(RepositoryFactory.GetRepository<OfferCategoryRepository>(), RepositoryFactory.GetRepository<OfferRepository>()),

                new ExitMenuAction()
            };

            return new InventoryParentActions(actions);
        }
    }
}
