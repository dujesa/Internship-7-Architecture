using PointOfSaleApp.Domain.Repositories.BillRepositories;
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
    public class BestSellingOffersReportAction : IAction
    {
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Top 10 best sellers";

        public BestSellingOffersReportAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var bestSellers = _offerRepository.GetBestSellersByCount(10);

            for (int i = 0; i < bestSellers.Count; i++)
            {
                var bestSeller = bestSellers.ElementAt(i);
                var offer = _offerRepository.FetchById(bestSeller.OfferId);

                Console.WriteLine($"\n\t#{i+1} Bestseller");
                ConsolePrinter.PrintOffer(offer);
                Console.WriteLine($"----------Qty. of sold units: {bestSeller.Count}");
            }
        }
    }
}
