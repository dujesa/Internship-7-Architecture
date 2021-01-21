using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Actions.OfferActions;
using System.Collections.Generic;

namespace PointOfSaleApp.Presentation.Factories
{
    public class OfferActionsFactory
    {
        public static OfferParentActions GetOfferParentActions()
        {
            var actions = new List<IAction>
            {
                new ArticleAddAction(RepositoryFactory.GetRepository<ArticleRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new ServiceAddAction(RepositoryFactory.GetRepository<ServiceRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new SubscriptionAddAction(RepositoryFactory.GetRepository<SubscriptionRepository>(RepositoryFactory.GetRepository<OfferRepository>())),

                new ArticleEditAction(RepositoryFactory.GetRepository<ArticleRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new ServiceEditAction(RepositoryFactory.GetRepository<ServiceRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                
                new ArticleDeleteAction(RepositoryFactory.GetRepository<ArticleRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new ServiceDeleteAction(RepositoryFactory.GetRepository<ServiceRepository>(RepositoryFactory.GetRepository<OfferRepository>())),

                new ExitMenuAction()
            };

            return new OfferParentActions(actions);
        }
    }
}
