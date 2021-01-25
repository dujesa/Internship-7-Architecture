using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.ReportsActions
{
    public class InventoryReportAction : IAction
    {
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Advanced inventory report";

        public InventoryReportAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            Console.Clear();
            Console.WriteLine("Please input quantity for comparision of offers on inventory");

            if (!ConsoleReader.IsNumberRead(out var comparingQuantity))
                return;

            var isGreaterThanInputQty = ConsoleReader.ConfirmAction("Do you wanna offers with quantity greater than your input\n" +
                "['yes' - greater] [leave blank - lesser]");

            var offers = _offerRepository.GetAllAvailableWithQuantityParameters(comparingQuantity, isGreaterThanInputQty);
            
            ConsolePrinter.PrintOffers(offers);
        }
    }
}
