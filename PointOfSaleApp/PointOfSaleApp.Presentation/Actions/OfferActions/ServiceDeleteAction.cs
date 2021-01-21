using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.OfferActions
{
    public class ServiceDeleteAction : IAction
    {
        private readonly ServiceRepository _serviceRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete service";

        public ServiceDeleteAction(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public void Call()
        {
            var services = _serviceRepository.GetAll();
            ConsolePrinter.ShortPrintServices(services);

            Console.WriteLine("\n Type article id(number) or any other key for exit");
            var isExitInputted = ConsoleReader.IsExitReadOnNumberInput(out var serviceId);
            if (isExitInputted)
                return;

            var response = _serviceRepository.Delete(serviceId);

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully deleted article\n");
            }

            if (response == ResponseResultType.NotFound)
            {
                Console.WriteLine("Article not found");
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
