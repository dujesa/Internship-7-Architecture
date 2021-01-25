using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Handlers;
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

        public Employee Employee { get; set; }

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Issue new bill";

        public IssueNewBillAction(Employee employee, BillRepository billRepository, BillItemRepository billItemRepository, OfferRepository offerRepository)
        {
            Employee = employee;
            _billRepository = billRepository;
            _billItemRepository = billItemRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            if (!ConsoleReader.ConfirmAction("Do you wanna issue new bill?"))
                return;

            (var billCreationResponse, var newBill) = _billRepository.CreateNew();

            if (billCreationResponse != ResponseResultType.Success)
            {
                Console.WriteLine("505 - Error occured in creation of new bill.");
            }

            var closeBill = false;
            var isCustomerDataNeeded = false;
            var isMonthlySubscription = false;
            Customer customer = null;

            while (!closeBill)
            {
                Console.Clear();
                if (!ConsoleReader.ConfirmAction("Does your customer wanna add more items to bill?"))
                {
                    closeBill = true;
                    break;
                }

                Console.Clear();
                isCustomerDataNeeded = HandleAddingBillItem(newBill);

                if (isCustomerDataNeeded)
                {
                    customer = ConsoleReader.ProvideCustomer();
                    isMonthlySubscription = ConsoleReader.ConfirmAction("" +
                        "Do you wanna monthly subscription?\n" +
                        "['yes' - monthly][leave blank - yearly]");

                    break;
                }
            }

            var isClosingRequested = ConsoleReader.ConfirmAction($"" +
                $"Do you want to close bill?\n" +
                $"['yes' - close bill]" +
                $"[Any other input - cancel bill]");

            if (isClosingRequested && customer is Customer)
                ClosingBillHandler.Handle(newBill, customer, isMonthlySubscription);
            else if (isClosingRequested)
                ClosingBillHandler.Handle(newBill, Employee);
            else
            {
                HandleCancellingBill(newBill);

                return;
            }

            //validate that item is not 0 qty - remove it

            var billItems = _billItemRepository.GetAllByBillId(newBill.Id);
            var billPickupTime = _billRepository.GetPickupTimeById(newBill.Id);


            if (billPickupTime == null)
                ConsolePrinter.DisplayBillWithItems(billItems);
            else
                ConsolePrinter.DisplayBillWithItems(billItems, (DateTime)billPickupTime);


            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private void HandleCancellingBill(Bill bill)
        {
            _billRepository.CancelById(bill.Id);

            Console.WriteLine("\nBill cancelled...");
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private bool HandleAddingBillItem(Bill bill)
        {
            var offers = _offerRepository.GetAll();
            var billItemCount = _billItemRepository.CountByBillId(bill.Id);

            if (billItemCount > 0)
            {
                offers = offers
                    .Where(o => o.OfferType != OfferType.Subscription)
                    .ToList();
            }

            Console.WriteLine($"List of all available offers:");
            ConsolePrinter.PrintOffers(offers);

            Console.WriteLine("\n Type offer id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var offerId);
            if (isExitInputted) return false;

            Console.Clear();

            var addingOffer = offers.Where(o => o.Id == offerId).FirstOrDefault();
            if (addingOffer == null)
            {
                Console.WriteLine("Offer not found, maybe you have inputted wrong id.");
                ConsolePrinter.ClearScreenWithSleep();
                
                return false;
            }

            if (addingOffer.OfferType == OfferType.Subscription && bill.BillItems.Count > 0)
            {
                Console.WriteLine("You have selected subscription offer and only one sub can be added to single bill!");
                ConsolePrinter.ClearScreenWithSleep();

                return false;
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

            (var quantityEditResponse, var increasedQuantity)= _billItemRepository.IncreaseQuantityByIdFor(billItem.Id, 1);
            ConsolePrinter.DisplayResponse(quantityEditResponse);

            if(quantityEditResponse == ResponseResultType.Success)
                Console.WriteLine($"\n\nBill item: {addingOffer.Name}" +
                    $"\nQuantity increased for {increasedQuantity}, still available qty. {addingOffer.AvailableQuantity}");

            ConsolePrinter.ClearScreenWithSleep(3000);

            return addingOffer.OfferType == OfferType.Subscription;
        }


    }
}
