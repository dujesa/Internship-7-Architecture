using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.ReportsActions
{
    public class BillsReportAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly BillRepository _billRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Bills reports";

        public BillsReportAction(OfferCategoryRepository offerCategoryRepository, BillRepository billRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _billRepository = billRepository;
        }

        public void Call()
        {
            Console.WriteLine("---Detailed bill reports---\n");

            (var startTime, var endTime) = ConsoleReader.ProvideDatePeriod();

            var offerCategories = _offerCategoryRepository.GetAll();
            Console.WriteLine("Input category id for categories you want to include in report:");
            ConsolePrinter.ShortPrintOfferCategories(offerCategories);

            var isCategoryInputted = false;
            List<int> reportCategoriesIds = new List<int>();
            
            do
            {
                isCategoryInputted = ConsoleReader.IsNumberRead(out var categoryId);
                if (reportCategoriesIds.Contains(categoryId))
                    continue;
                if (isCategoryInputted)
                    reportCategoriesIds.Add(categoryId);
            } while (isCategoryInputted);

            var bills = _billRepository.GetAllIssuedInsideByOfferCategoryIds(startTime, endTime, reportCategoriesIds);

            foreach (var bill in bills)
            {
                ConsolePrinter.DisplayBill(bill);    
            }

            Console.WriteLine();
        }
    }
}
