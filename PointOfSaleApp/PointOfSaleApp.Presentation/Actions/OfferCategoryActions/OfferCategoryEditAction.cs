using PointOfSaleApp.Data.Entities.Models;
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
    public class OfferCategoryEditAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Offer category edit";

        public OfferCategoryEditAction(OfferCategoryRepository offerCategoryRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
        }

        public void Call()
        {
            var offerCategories = _offerCategoryRepository.GetAll();
            ConsolePrinter.ShortPrintOfferCategories(offerCategories);

            Console.WriteLine("\n Type offer category id(number) or any other key for exit");
            var isNumberInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerCategoryId);
            if (isNumberInputted)
                return;

            var offerCategory = offerCategories.FirstOrDefault(oc => oc.Id == offerCategoryId);
            if (!(offerCategory is OfferCategory))
            {
                Console.WriteLine("Category was not found");
                ConsolePrinter.ClearScreenWithSleep();
            }

            Console.WriteLine($"Category name: [{offerCategory.Name}]");
            offerCategory.Name = ConsoleReader.IsLineRead(out var name)
                ? name
                : offerCategory.Name;

            var response = _offerCategoryRepository.Edit(offerCategory, offerCategoryId);

            Console.Clear();

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully edited category\n");
                ConsolePrinter.PrintOfferCategory(offerCategory);
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
