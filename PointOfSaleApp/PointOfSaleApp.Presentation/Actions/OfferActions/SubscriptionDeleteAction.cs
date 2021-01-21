using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class SubscriptionDeleteAction : IAction
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete subscription";

        public SubscriptionDeleteAction(SubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void Call()
        {
            var subscriptions = _subscriptionRepository.GetAll();
            ConsolePrinter.ShortPrintSubscriptions(subscriptions);

            Console.WriteLine("\n Type subscription id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var subscriptionId);
            if (isExitInputted)
                return;

            var response = _subscriptionRepository.Delete(subscriptionId);

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully deleted subscription\n");
            }

            if (response == ResponseResultType.NotFound)
            {
                Console.WriteLine("Subscription not found");
            }

            if (response == ResponseResultType.NoChanges)
            {
                Console.WriteLine("No changes applied");
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
