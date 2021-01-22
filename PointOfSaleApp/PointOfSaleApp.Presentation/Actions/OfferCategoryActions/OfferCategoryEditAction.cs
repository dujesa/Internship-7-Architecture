using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
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
    public class OfferCategoryEditAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Offer category edit";

        public OfferCategoryEditAction(OfferCategoryRepository offerCategoryRepository, OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _offerRepository = offerRepository;
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

            Console.WriteLine("" +
                "Do you wanna add or remove offers from category?\n" +
                "0 - Do not add or remove offers\n" +
                "1 - Add offer\n" +
                "2 - Remove offer");
            var skipOfferAdition = ConsoleReader.IsExitReadOnNumberInput(out var offerAdditionChoice);
            if (!skipOfferAdition)
            {
                if (offerAdditionChoice == 1) HandleAdditionOfOfferToCategory(offerCategory);
                if (offerAdditionChoice == 2) HandleRemoveOfOfferFromCategory(offerCategory);
            }



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

        private void HandleAdditionOfOfferToCategory(OfferCategory offerCategory)
        {
            Console.WriteLine($"List of all offers:");
            var offers = _offerRepository.GetAll();
            ConsolePrinter.PrintOffers(offers);

            Console.WriteLine("\n Type offer id(number) or any other key for exit");
            while (!ConsoleReader.IsExitReadOnNumberInput(out var categoryId))
            {
                var addingOffer = offers.Where(o => o.Id == categoryId).FirstOrDefault();

                if (addingOffer is Offer && offerCategory is OfferCategory)
                {
                    //validate that does not exist
                    offerCategory.Offers.Add(addingOffer);
                    addingOffer.OfferCategories.Add(offerCategory);
                }
                else
                {
                    Console.WriteLine("Offer not found");
                }

                Console.WriteLine("\n Type offer id(number) or any other key for exit");
            }
        }

        private void HandleRemoveOfOfferFromCategory(OfferCategory offerCategory)
        {

            Console.WriteLine($"Category offers:");
            ConsolePrinter.PrintOffers(offerCategory.Offers);

            Console.WriteLine("\n Type offer id(number) or any other key for exit");
            while (!ConsoleReader.IsExitReadOnNumberInput(out var removingId))
            {
                var removingOffer = offerCategory.Offers
                    .Where(o => o.Id == removingId)
                    .FirstOrDefault();

                if (removingOffer is Offer && offerCategory is OfferCategory)
                {
                    offerCategory.Offers.Remove(removingOffer);
                    removingOffer.OfferCategories.Remove(offerCategory);
                }
                else
                {
                    Console.WriteLine("Offer not found");
                }

                Console.WriteLine("\n Type offer id(number) or any other key for exit");
            }
        }
    }
}
