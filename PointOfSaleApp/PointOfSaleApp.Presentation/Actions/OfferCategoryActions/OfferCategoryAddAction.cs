using PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferCategoryActions
{
    public class OfferCategoryAddAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Offer category add";

        public OfferCategoryAddAction(OfferCategoryRepository offerCategoryRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
        }

        public void Call()
        {
            Console.WriteLine("Enter name:");
            var name = Console.ReadLine();

            (var response, var message) = _offerCategoryRepository.Add(name);

            Console.WriteLine(message);

            //do you wanna offer management
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
