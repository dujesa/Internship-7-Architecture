using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.BillActions
{
    public class IssueNewBillAction : IAction
    {
        private readonly BillRepository _billRepository;
        private readonly BillItemRepository _billItemRepository;
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Issue new bill";

        public IssueNewBillAction(BillRepository billRepository, BillItemRepository billItemRepository, OfferRepository offerRepository)
        {
            _billRepository = billRepository;
            _billItemRepository = billItemRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            if (!ConsoleReader.ConfirmAction("Do you wanna issue new bill?"))
                return;

            //todo: napravi factory
            (var billCreationResponse, var newBill) = _billRepository.CreateNew();

            if (billCreationResponse != ResponseResultType.Success)
            {
                Console.WriteLine("505 - Error occured in creation of new bill.");
            }

            var closeBill = false;

            while (!closeBill)
            {
                Console.Clear();
                if (!ConsoleReader.ConfirmAction("Does your customer wanna add more items to bill?"))
                {
                    closeBill = true;
                    break;
                }

                Console.Clear();
                HandleAddingBillItem(newBill);
            }

            //ask for closing bill or cancelling it
            //HandleCancellingBill
            //HandleClosingBill

            //Console.WriteLine(message);
            //DISPLAY BILL
            var billItems = _billItemRepository.GetAllByBillId(newBill.Id);
            ConsolePrinter.DisplayBillWithItems(billItems);

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        
        private void HandleAddingBillItem(Bill bill)
        {
            var offers = _offerRepository.GetAll();
            
            Console.WriteLine($"List of all offers:");
            ConsolePrinter.PrintOffers(offers);

            Console.WriteLine("\n Type offer id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerId);
            if (isExitInputted) return;

            Console.Clear();

            var addingOffer = offers.Where(o => o.Id == offerId).FirstOrDefault();
            if (addingOffer == null)
            {
                Console.WriteLine("Offer not found, maybe you have inputted wrong id.");
                ConsolePrinter.ClearScreenWithSleep();
                
                return;
            }

            var billItem = _billItemRepository.GetByOfferIdAndBillId(offerId, bill.Id);
                
            if (billItem == null)
            {
                (var billItemAdditionResponse, var newBillItem) = _billItemRepository.Add(bill.Id, offerId);
                billItem = newBillItem;

                ConsolePrinter.DisplayResponse(billItemAdditionResponse);
            }

            if (billItem == null)
            {
                Console.WriteLine("505 - Error occurred in fetching of bill item.");
            }

            var quantityEditResponse = _billItemRepository.IncreaseQuantityByIdFor(billItem.Id, 1);
            ConsolePrinter.DisplayResponse(quantityEditResponse);
        }
    }
}
