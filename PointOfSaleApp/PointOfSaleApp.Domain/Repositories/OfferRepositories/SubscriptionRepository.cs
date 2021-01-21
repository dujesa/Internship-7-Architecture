using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSaleApp.Domain.Repositories.OfferRepositories
{
    public class SubscriptionRepository : BaseRepository
    {
        private readonly OfferRepository _offerRepository;

        public SubscriptionRepository(PointOfSaleDbContext dbContext, OfferRepository offerRepository) : base(dbContext)
        {
            _offerRepository = offerRepository;
        }

        public (ResponseResultType Result, string Message) Add(string name, string description, int availableQuantity, decimal pricePerDay)
        {
            (var parentResult, var message, var offer) = _offerRepository.Add(name, description, availableQuantity);

            if (!(offer is Offer))
                return (parentResult, message);

            if (pricePerDay < 0.0m)
                return (ResponseResultType.ValidationError, "Price per day for subscription must be greater than 0.0");

            var subscription = new Subscription
            { 
                PricePerDay = pricePerDay,
                Offer = DbContext.Offers.Find(offer.Id)
            };

            subscription.Offer.OfferType = OfferType.Subscription;

            DbContext.Subscriptions.Add(subscription);

            var result = SaveChanges();
            return (result == ResponseResultType.Success) ? (ResponseResultType.Success, "Succesfully added subscription.") : (ResponseResultType.NoChanges, "No changes made.");
        }

        public ResponseResultType Edit(Subscription subscription, int subscriptionId)
        {
            var edittingSubscription = DbContext.Subscriptions.Find(subscriptionId);

            if (edittingSubscription == null)
            {
                return ResponseResultType.NotFound;
            }

            _offerRepository.Edit(subscription.Offer, subscription.OfferId);

            edittingSubscription.PricePerDay = subscription.PricePerDay;

            return SaveChanges();
        }

        public ResponseResultType Delete(int subscriptionId)
        {
            var subscription = DbContext.Subscriptions.Find(subscriptionId);

            if (subscription == null)
                return ResponseResultType.NotFound;

            DbContext.Subscriptions.Remove(subscription);

            var result = SaveChanges();

            if (result != ResponseResultType.Success)
                return result;

            return _offerRepository.Delete(subscription.OfferId);
        }

        public ICollection<Subscription> GetAll()
        {
            return DbContext.Subscriptions
                .Include(s => s.Offer)
                .ToList();
        }
    }
}
