using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSaleApp.Domain.Repositories.OfferRepositories
{
    public class OfferRepository : BaseRepository
    {
        public OfferRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public (ResponseResultType Result, string Message, Offer offer) Add(string name, string description, int availableQuantity)
        {
            if (availableQuantity < 0)
                return (ResponseResultType.ValidationError, "Available quantity cannot be negative number!", null);

            if (DbContext.Offers.Any(o => o.Name == name))
                return (ResponseResultType.AlreadyExists, $"Offer with name: {name} already exists in POS.", null);

            var offer = new Offer
            { 
                Name = name,
                Description = description,
                AvailableQuantity = availableQuantity
            };

            DbContext.Offers.Add(offer);
            var result = SaveChanges();

            return (result == ResponseResultType.Success) ? (result, "Offer has been successfully added.", offer) : (result, "No changes made, no new offer added.", null);
        }
    }
}
