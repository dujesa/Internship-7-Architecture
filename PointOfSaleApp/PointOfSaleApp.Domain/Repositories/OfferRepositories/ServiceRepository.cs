using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;

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
    }
}
