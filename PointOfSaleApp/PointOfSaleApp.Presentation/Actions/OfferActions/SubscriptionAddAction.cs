using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class SubscriptionAddAction : IAction
    {
        private readonly SubscriptionRepository _subscriptionRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add new subscription";

        public SubscriptionAddAction(SubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void Call()
        {
            //toDo: Print option to choose offer category
            // 1. - repo get all
            // 2. - pretty print categories
            // 3. - get number for category

            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter description:");
            var description = Console.ReadLine();

            Console.WriteLine("Enter quantity available:");
            var isQuantityRead = false;
            var quantityAvailable = 0;
            while (!isQuantityRead)
            {
                isQuantityRead = int.TryParse(Console.ReadLine(), out quantityAvailable);
            }

            Console.WriteLine("Enter price per day:");
            var isPriceRead = false;
            var pricePerDay = 0.0m;
            while (!isPriceRead)
            {
                isPriceRead = decimal.TryParse(Console.ReadLine(), out pricePerDay);
            }

            var result = _subscriptionRepository.Add(name, description, quantityAvailable, pricePerDay);

            Console.WriteLine(result.Message);

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
