using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
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
    public class InventoryDeleteAction : IAction
    {
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Remove offer from inventory";

        public InventoryDeleteAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var isExitInputted = false;

            do
            {
                var availableOffers = _offerRepository.GetAvailable();

                Console.WriteLine($"List of all available offers:");
                ConsolePrinter.PrintOffers(availableOffers);

                Console.WriteLine("\n Type offer id(number) or any other key for exit");
                isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerId);
                if (isExitInputted) break;

                Console.Clear();

                var offer = availableOffers.Where(o => o.Id == offerId).FirstOrDefault();
                if (!(offer is Offer))
                {
                    Console.WriteLine("Offer not found or not avaiable in inventory.");
                    continue;
                }


                var response = _offerRepository.UpdateAvailableQuantityById(0, offerId);
                ConsolePrinter.DisplayResponse(response);

            } while (!isExitInputted);
        }
    }
}
