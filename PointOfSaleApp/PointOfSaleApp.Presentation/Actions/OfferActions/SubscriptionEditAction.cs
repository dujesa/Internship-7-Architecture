using PointOfSaleApp.Data.Entities.Models;
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
    public class SubscriptionEditAction : IAction
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit subscription";

        public SubscriptionEditAction(SubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void Call()
        {
            var subscriptions = _subscriptionRepository.GetAll();
            ConsolePrinter.ShortPrintSubscriptions(subscriptions);

            Console.WriteLine("\n Type subscription id(number) or any other key for exit");
            var isNumberInputted = ConsoleReader.IsExitReadOnNumberInput(out var subscriptionId);
            if (isNumberInputted)
                return;

            var subscription = subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
            if (!(subscription is Subscription))
            {
                Console.WriteLine("Subscription was not found");
                ConsolePrinter.ClearScreenWithSleep();
            }

            var offer = subscription.Offer;

            Console.WriteLine("Press enter to skip editting displayed field.");

            Console.WriteLine($"Subscription name: [{offer.Name}]");
            offer.Name = ConsoleReader.IsLineRead(out var name)
                ? name
                : offer.Name;

            Console.WriteLine($"Subscription description: [{offer.Description}]");
            offer.Description = ConsoleReader.IsLineRead(out var description)
                ? description
                : offer.Description;

            Console.WriteLine($"Subscription quantity available: [{offer.AvailableQuantity}]");
            offer.AvailableQuantity = ConsoleReader.IsNumberRead(out var quantity)
                ? quantity
                : offer.AvailableQuantity;

            Console.WriteLine($"Subscription price per day: [{subscription.PricePerDay}]");
            subscription.PricePerDay = ConsoleReader.IsDecimalRead(out var pricePerDay)
                ? pricePerDay
                : subscription.PricePerDay;

            var response = _subscriptionRepository.Edit(subscription, subscriptionId);

            Console.Clear();

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully edited subscription\n");
                ConsolePrinter.DisplaySubscription(subscription);
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
