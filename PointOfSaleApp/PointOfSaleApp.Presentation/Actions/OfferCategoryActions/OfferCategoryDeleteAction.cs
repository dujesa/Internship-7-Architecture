using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferCategoryActions
{
    public class OfferCategoryDeleteAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Offer category delete";

        public OfferCategoryDeleteAction(OfferCategoryRepository offerCategoryRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
        }

        public void Call()
        {
            var offerCategories = _offerCategoryRepository.GetAll();
            ConsolePrinter.ShortPrintOfferCategories(offerCategories);

            Console.WriteLine("\n Type category id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerCategoryId);
            if (isExitInputted)
                return;

            var response = _offerCategoryRepository.Delete(offerCategoryId);

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully deleted category\n");
            }

            if (response == ResponseResultType.NotFound)
            {
                Console.WriteLine("Category not found");
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
