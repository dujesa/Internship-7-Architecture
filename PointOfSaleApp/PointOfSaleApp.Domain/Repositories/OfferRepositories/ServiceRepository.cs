using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSaleApp.Domain.Repositories.OfferRepositories
{
    public class ServiceRepository : BaseRepository
    {
        private readonly OfferRepository _offerRepository;

        public ServiceRepository(PointOfSaleDbContext dbContext, OfferRepository offerRepository) : base(dbContext)
        {
            _offerRepository = offerRepository;
        }

        public (ResponseResultType Result, string Message) Add(string name, string description, int availableQuantity, decimal pricePerHour, int workingHoursNeeded)
        {
            (var parentResult, var message, var offer) = _offerRepository.Add(name, description, availableQuantity);

            if (!(offer is Offer))
                return (parentResult, message);

            if (pricePerHour < 0.0m)
                return (ResponseResultType.ValidationError, "Price per hour for working on service must not be less than 0.0");

            if(workingHoursNeeded < 0)
                return (ResponseResultType.ValidationError, "Number of working hours for service must not be less than 0");

            var service = new Service
            { 
                PricePerHour = pricePerHour,
                WorkingHoursNeeded = workingHoursNeeded,
                Offer = DbContext.Offers.Find(offer.Id)
            };

            service.Offer.OfferType = OfferType.Service;

            DbContext.Services.Add(service);

            var result = SaveChanges();
            return (result == ResponseResultType.Success) ? (ResponseResultType.Success, "Succesfully added service.") : (ResponseResultType.NoChanges, "No changes made.");
        }

        public ResponseResultType Edit(Service service, int serviceId)
        {
            var edittingService = DbContext.Services.Find(serviceId);

            if (edittingService == null)
            {
                return ResponseResultType.NotFound;
            }

            _offerRepository.Edit(service.Offer, service.OfferId);

            edittingService.PricePerHour = service.PricePerHour;
            edittingService.WorkingHoursNeeded = service.WorkingHoursNeeded;

            return SaveChanges();
        }

        public ResponseResultType Delete(int serviceId)
        {
            var service = DbContext.Services.Find(serviceId);

            if (service == null)
                return ResponseResultType.NotFound;

            DbContext.Services.Remove(service);

            var result = SaveChanges();

            if (result != ResponseResultType.Success)
                return result;

            return _offerRepository.Delete(service.OfferId);
        }

        public ICollection<Service> GetAll()
        {
            return DbContext.Services
                .Include(a => a.Offer)
                .ToList();
        }
    }
}
