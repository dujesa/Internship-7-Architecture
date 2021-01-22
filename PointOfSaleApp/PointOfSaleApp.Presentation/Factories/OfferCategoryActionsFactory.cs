using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Actions.OfferCategoryActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Factories
{
    public class OfferCategoryActionsFactory
    {
        public static OfferCategoryParentActions GetOfferCategoryParentActions()
        {
            var actions = new List<IAction>
            {
                new OfferCategoryAddAction(RepositoryFactory.GetRepository<OfferCategoryRepository>()),
                new OfferCategoryEditAction(RepositoryFactory.GetRepository<OfferCategoryRepository>()),
                new OfferCategoryDeleteAction(RepositoryFactory.GetRepository<OfferCategoryRepository>()),
                //new OfferCategoryManagementAction(RepositoryFactory.GetRepository<SubscriptionRepository>(RepositoryFactory.GetRepository<OfferRepository>())),

                new ExitMenuAction()
            };

            return new OfferCategoryParentActions(actions);
        }
    }
}
