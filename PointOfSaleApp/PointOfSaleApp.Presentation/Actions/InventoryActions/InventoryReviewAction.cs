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
    public class InventoryReviewAction : IAction
    {
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "List offers available in inventory";

        public InventoryReviewAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            Console.Clear();

            var availableOffers = _offerRepository.GetAvailable();

            Console.WriteLine($"List of all available offers in inventory:");
            ConsolePrinter.PrintOffers(availableOffers);
        }
    }
}
