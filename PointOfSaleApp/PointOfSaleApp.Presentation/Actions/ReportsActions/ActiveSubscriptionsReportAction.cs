using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.ReportsActions
{
    public class ActiveSubscriptionsReportAction : IAction
    {
        private readonly SubscriptionBillRepository _subscriptionBillRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Report active subscriptions";

        public ActiveSubscriptionsReportAction(SubscriptionBillRepository subscriptionBillRepository)
        {
            _subscriptionBillRepository = subscriptionBillRepository;
        }

        public void Call()
        {
            var activeSubscriptionOffers = _subscriptionBillRepository.GetAllActive();

            ConsolePrinter.PrintOffers(activeSubscriptionOffers);
        }
    }
}
