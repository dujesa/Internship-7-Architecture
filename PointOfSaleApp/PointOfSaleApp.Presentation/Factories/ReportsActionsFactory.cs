using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Actions;
using PointOfSaleApp.Presentation.Actions.ReportsActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Factories
{
    public class ReportsActionsFactory
    {
        public static ReportsParentActions GetReportsParentActions()
        {
            var actions = new List<IAction>
            {
                //new BillsReportAction(RepositoryFactory.GetRepository<BillRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                //new ArticlesByCategoryReportAction(RepositoryFactory.GetRepository<ServiceRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new ActiveSubscriptionsReportAction(RepositoryFactory.GetRepository<SubscriptionBillRepository>()),
                //new InventoryReportAction(RepositoryFactory.GetRepository<SubscriptionRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                //new BestSellingOffersReportAction(RepositoryFactory.GetRepository<SubscriptionRepository>(RepositoryFactory.GetRepository<OfferRepository>())),
                new AnnualProfitReportAction(RepositoryFactory.GetRepository<BillRepository>()),

                new ExitMenuAction()
            };

            return new ReportsParentActions(actions);
        }
    }
}
