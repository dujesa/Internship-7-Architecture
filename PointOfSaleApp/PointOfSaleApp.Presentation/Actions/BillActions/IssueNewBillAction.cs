using PointOfSaleApp.Data.Entities.Models;
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
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Issue new bill";

        public IssueNewBillAction(BillRepository billRepository, OfferRepository offerRepository)
        {
            _billRepository = billRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            Console.WriteLine("Do you wanna issue new bill (input 'yes' to confirm)");
            var isInputted = ConsoleReader.IsLineRead(out string input);
            if (!isInputted || !input.Equals("yes"))
            {
                Console.Clear();
                
                return;
            }

            var newBill = new Bill
            {
                Price = 0,
                IssuedAt = DateTime.Now,
            };

            (var response, var message) = _billRepository.CreateNew();

            Console.WriteLine(message);

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
