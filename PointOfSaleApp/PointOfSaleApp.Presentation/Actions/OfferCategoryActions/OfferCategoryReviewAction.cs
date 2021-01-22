using PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferCategoryActions
{
    public class OfferCategoryReviewAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Offer category review";

        public OfferCategoryReviewAction(OfferCategoryRepository offerCategoryRepository, OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var offerCategories = _offerCategoryRepository.GetAll();
            ConsolePrinter.ShortPrintOfferCategories(offerCategories);

            Console.WriteLine("\n Type category id(number) or any other key for exit");
            bool isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerCategoryId);
            if (isExitInputted)
                return;

            var category = _offerCategoryRepository.GetById(offerCategoryId);
            var categoryOffers = _offerRepository.GetAllByCategoryId(offerCategoryId);


            Console.Clear();
            Console.WriteLine("Category:");
            ConsolePrinter.PrintOfferCategory(category);

            Console.WriteLine("\nCategory offers:");
            if (categoryOffers.Count == 0)
                Console.WriteLine("Category does not contain any offer.");
            else
                ConsolePrinter.PrintOffers(categoryOffers);

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
