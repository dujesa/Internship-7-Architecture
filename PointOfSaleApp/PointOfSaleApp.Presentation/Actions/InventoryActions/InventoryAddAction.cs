using PointOfSaleApp.Data.Entities.Models;
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
    public class InventoryAddAction : IAction
    {
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add offer to inventory";

        public InventoryAddAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var isExitInputted = false;

            do
            {
                var soldOutOffers = _offerRepository.GetSoldOut();

                Console.WriteLine($"List of all sold out offers:");
                ConsolePrinter.PrintOffers(soldOutOffers);

                Console.WriteLine("\n Type offer id(number) or any other key for exit");
                isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerId);
                if (isExitInputted) break;

                Console.Clear();

                var offer = soldOutOffers.Where(o => o.Id == offerId).FirstOrDefault();
                if (!(offer is Offer))
                {
                    Console.WriteLine("Offer not found or not sold out.");
                    continue;
                }

                Console.WriteLine($"Enter quantity you want to set on offer {offer.Name}:");
                offer.AvailableQuantity = ConsoleReader.IsNumberRead(out var quantity)
                    ? quantity
                    : offer.AvailableQuantity;

                var response = _offerRepository.Edit(offer, offerId);
                ConsolePrinter.DisplayResponse(response);

            } while (!isExitInputted);
        }
    }
}
