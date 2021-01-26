using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
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
                new BillsReportAction(RepositoryFactory.GetRepository<OfferCategoryRepository>(), RepositoryFactory.GetRepository<BillRepository>()),
                //new CountSoldOffersByCategoryReportAction(RepositoryFactory.GetRepository<BillItemRepository>()),
                new ActiveSubscriptionsReportAction(RepositoryFactory.GetRepository<SubscriptionBillRepository>()),
                new InventoryReportAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new BestSellingOffersReportAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new AnnualProfitReportAction(RepositoryFactory.GetRepository<BillRepository>()),

                new ExitMenuAction()
            };

            return new ReportsParentActions(actions);
        }
    }
}
