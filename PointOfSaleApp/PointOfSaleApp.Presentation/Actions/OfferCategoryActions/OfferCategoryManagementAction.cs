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
    class OfferCategoryManagementAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Category offers management";

        public OfferCategoryManagementAction(OfferCategoryRepository offerCategoryRepository, OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var offerCategories = _offerCategoryRepository.GetAll();
            ConsolePrinter.ShortPrintOfferCategories(offerCategories);
            
            Console.WriteLine("\n Type offer category id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerCategoryId);
            if (isExitInputted)
                return;

            Console.WriteLine("" +
                "Do you wanna add or remove offers from category?\n" +
                "0 - Do not add or remove offers\n" +
                "1 - Add offer\n" +
                "2 - Remove offer");

            var skipOfferAdition = ConsoleReader.IsExitReadOnNumberInput(out var offerAdditionChoice);
            if (!skipOfferAdition)
            {
                if (offerAdditionChoice == 1) HandleAdditionOfOfferToCategory(offerCategories, offerCategoryId);
                if (offerAdditionChoice == 2) HandleRemoveOfOfferFromCategory(offerCategories, offerCategoryId);
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private void HandleAdditionOfOfferToCategory(ICollection<OfferCategory> offerCategories, int offerCategoryId)
        {
            var offerCategory = offerCategories.FirstOrDefault(oc => oc.Id == offerCategoryId);

            if (!(offerCategory is OfferCategory))
            {
                Console.WriteLine("Category was not found");
                ConsolePrinter.ClearScreenWithSleep();

                return;
            }

            var offers = _offerRepository.GetAll();
            var isExitInputted = false;

            do
            {
                Console.WriteLine($"List of all offers:");
                ConsolePrinter.PrintOffers(offers);

                Console.WriteLine("\n Type offer id(number) or any other key for exit");
                isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerId);
                if (isExitInputted) break;

                Console.Clear();

                var addingOffer = offers.Where(o => o.Id == offerId).FirstOrDefault();
                if (!(addingOffer is Offer) || !(offerCategory is OfferCategory))
                {
                    Console.WriteLine("Offer not found");
                    continue;
                }
                    
                var hasOffer = _offerCategoryRepository.HasOffer(offerCategory, addingOffer);
                if (hasOffer)
                {
                    Console.WriteLine("Offer is already contained in this category!");
                    continue;
                }
                
                var response = _offerCategoryRepository.AddOffer(addingOffer, offerCategory, offerCategories);
                ConsolePrinter.DisplayResponse(response);

            } while (!isExitInputted);
        }

        private void HandleRemoveOfOfferFromCategory(ICollection<OfferCategory> offerCategories, int offerCategoryId)
        {
            var offerCategory = offerCategories.FirstOrDefault(oc => oc.Id == offerCategoryId);

            if (!(offerCategory is OfferCategory))
            {
                Console.WriteLine("Category was not found");
                ConsolePrinter.ClearScreenWithSleep();

                return;
            }

            var isExitInputted = false;
            do {
                Console.WriteLine($"Category offers:");
                ConsolePrinter.PrintOffers(offerCategory.Offers);
                
                Console.WriteLine("\n Type offer id(number) or any other key for exit");
                isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var removingId);
                if (isExitInputted) break;

                Console.Clear();

                var removingOffer = offerCategory.Offers
                    .Where(o => o.Id == removingId)
                    .FirstOrDefault();


                if (!(removingOffer is Offer) || !(offerCategory is OfferCategory))
                {
                    Console.WriteLine("Offer not found");
                    continue;
                }
                
                offerCategory.Offers.Remove(removingOffer);
                removingOffer.OfferCategories.Remove(offerCategory);

                var response = _offerCategoryRepository.Edit(offerCategory, offerCategoryId);
                ConsolePrinter.DisplayResponse(response);

            } while (!isExitInputted);
        }
    }
}
