using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.InventoryActions
{
    public class AvailableSubscriptionsInventoryAction : IAction
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Review subscription inventory";

        public AvailableSubscriptionsInventoryAction(SubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void Call()
        {
            Console.Clear();

            var availableSubscriptions = _subscriptionRepository.GetAvailable();

            Console.WriteLine($"List of all available subscriptions in inventory:");
            ConsolePrinter.PrintSubscriptions(availableSubscriptions);
        }
    }
}
