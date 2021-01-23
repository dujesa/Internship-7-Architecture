using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Presentation.Abstractions;
using PointOfSaleApp.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleApp.Presentation.Actions.InventoryActions
{
    public class AvailableServicesInventoryAction : IAction
    {
        private readonly ServiceRepository _serviceRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Review service inventory";

        public AvailableServicesInventoryAction(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public void Call()
        {
            Console.Clear();

            var availableServices = _serviceRepository.GetAvailable();

            Console.WriteLine($"List of all available services in inventory:");
            ConsolePrinter.PrintServices(availableServices);
        }
    }
}
