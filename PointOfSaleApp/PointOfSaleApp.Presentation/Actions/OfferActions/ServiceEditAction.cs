using PointOfSaleApp.Data.Entities.Models;
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
    public class ServiceEditAction : IAction
    {
        private readonly ServiceRepository _serviceRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit service";

        public ServiceEditAction(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public void Call()
        {
            var services = _serviceRepository.GetAll();
            ConsolePrinter.ShortPrintServices(services);

            Console.WriteLine("\n Type service id(number) or any other key for exit");
            var isNumberInputted = ConsoleReader.IsExitReadOnNumberInput(out var serviceId);
            if (isNumberInputted)
                return;

            var service = services.FirstOrDefault(s => s.Id == serviceId);
            if (!(service is Service))
            {
                Console.WriteLine("Service was not found");
                ConsolePrinter.ClearScreenWithSleep();
            }

            var offer = service.Offer;

            Console.WriteLine("Press enter to skip editting displayed field.");

            Console.WriteLine($"Service name: [{offer.Name}]");
            offer.Name = ConsoleReader.IsLineRead(out var name)
                ? name
                : offer.Name;

            Console.WriteLine($"Service description: [{offer.Description}]");
            offer.Description = ConsoleReader.IsLineRead(out var description)
                ? description
                : offer.Description;

            Console.WriteLine($"Service quantity available: [{offer.AvailableQuantity}]");
            offer.AvailableQuantity = ConsoleReader.IsNumberRead(out var quantity)
                ? quantity
                : offer.AvailableQuantity;

            Console.WriteLine($"Service price per hour: [{service.PricePerHour}]");
            service.PricePerHour = ConsoleReader.IsDecimalRead(out var price)
                ? price
                : service.PricePerHour;            
            
            Console.WriteLine($"Service working hours needed: [{service.WorkingHoursNeeded}]");
            service.WorkingHoursNeeded = ConsoleReader.IsNumberRead(out var hours)
                ? hours
                : service.WorkingHoursNeeded;

            var response = _serviceRepository.Edit(service, serviceId);

            Console.Clear();

            if (response == ResponseResultType.Success)
            {
                Console.WriteLine("Succesfully edited article\n");
                ConsolePrinter.DisplayService(service);
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
