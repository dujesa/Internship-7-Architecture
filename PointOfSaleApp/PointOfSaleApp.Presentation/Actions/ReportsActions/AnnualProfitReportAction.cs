using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.ReportsActions
{
    public class AnnualProfitReportAction : IAction
    {
        private readonly BillRepository _billRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Report annual profit";

        public AnnualProfitReportAction(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public void Call()
        {
            Console.Clear();
            Console.WriteLine("Please input year for annual profit report");
            
            if (!ConsoleReader.IsNumberRead(out var reportYear))
                return;

            var annualProfit = _billRepository.SumProfitByYear(reportYear);

            Console.WriteLine($"Profit for year {reportYear} is {annualProfit}");
        }
    }
}
