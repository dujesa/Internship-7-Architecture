using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Models;
using System;
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

        public ICollection<Offer> GetAllAvailableWithQuantityParameters(int comparingQuantity, bool isGreaterThanInputQty)
        {
            return (isGreaterThanInputQty)
                ? DbContext.Offers
                    .Where(o => o.AvailableQuantity > comparingQuantity)
                    .ToList()
                : DbContext.Offers
                    .Where(o => o.AvailableQuantity < comparingQuantity)
                    .ToList();
        }

        public ICollection<BestSeller> GetBestSellersByCount(int takeCount)
        {
            var bestSellers = DbContext.BillItems
                .Include(bi => bi.Bill)
                .Where(bi => bi.Bill.IsCancelled == false)
                .GroupBy(bi => bi.OfferId)
                .Select(bi => new BestSeller
                {
                    OfferId = bi.Key,
                    Count = bi.Count()
                })
                .Take(takeCount)
                .ToList();

            return bestSellers.OrderByDescending(bs => bs.Count).ToList();
        }

        public ICollection<Offer> GetAvailable()
        {
            return DbContext.Offers
                .Where(o => o.AvailableQuantity > 0)
                .ToList();
        }

        public ICollection<Offer> GetSoldOut()
        {
            return DbContext.Offers
                .Where(o => o.AvailableQuantity <= 0)
                .ToList();
        }

        public ResponseResultType UpdateAvailableQuantityById(int availableQuantity, int offerId)
        {
            var edittingOffer = DbContext.Offers
                .Include(o => o.Articles)
                .Include(o => o.Services)
                .Include(o => o.Subscriptions)
                .Where(o => o.Id == offerId)
                .FirstOrDefault();

            if (edittingOffer == null)
            {
                return ResponseResultType.NotFound;
            }

            edittingOffer.AvailableQuantity = availableQuantity;

            return SaveChanges();
        }

        public ResponseResultType Edit(Offer offer, int offerId)
        {
            var edittingOffer = DbContext.Offers.Find(offerId);

            if(edittingOffer == null)
            {
                return ResponseResultType.NotFound;
            }

            edittingOffer.Name = offer.Name;
            edittingOffer.Description = offer.Description;
            edittingOffer.AvailableQuantity = offer.AvailableQuantity;

            return SaveChanges();
        }

        public Offer GetById(int id)
        {
            return DbContext.Offers.Find(id);
        }

        public Offer FetchById(int id)
        {
            return DbContext.Offers
                .Include(o => o.Articles)
                .Include(o => o.Services)
                .Include(o => o.Subscriptions)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public ResponseResultType Delete(int offerId)
        {
            var offer = DbContext.Offers.Find(offerId);

            if (offer == null)
                return ResponseResultType.NotFound;

            DbContext.Offers.Remove(offer);

            return SaveChanges();
        }

        public ICollection<Offer> GetAllByCategoryId(int categoryId)
        {
            var category = DbContext.OfferCategories
                .Include(oc => oc.Offers).ThenInclude(o => o.Articles)
                .Include(oc => oc.Offers).ThenInclude(o => o.Subscriptions)
                .Include(oc => oc.Offers).ThenInclude(o => o.Services)
                .Where(oc => oc.Id == categoryId)
                .FirstOrDefault();

            return (category is OfferCategory)
                ? category.Offers
                : new List<Offer>();
        }

        public ICollection<Offer> GetAll()
        {

            return DbContext.Offers
                .ToList();
        }
    }
}
