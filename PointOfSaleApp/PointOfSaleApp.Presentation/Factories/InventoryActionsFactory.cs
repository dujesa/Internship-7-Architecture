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
                new InventoryDeleteAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new InventoryReviewAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new InventoryQuantityManagementAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new AvailableArticlesInventoryAction(RepositoryFactory.GetRepository<ArticleRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new AvailableSubscriptionsInventoryAction(RepositoryFactory.GetRepository<SubscriptionRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new AvailableServicesInventoryAction(RepositoryFactory.GetRepository<ServiceRepository>(RepositoryFactory.GetRepository<OfferRepository>())),

                new ExitMenuAction()
            };

            return new InventoryParentActions(actions);
        }
    }
}
