using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.OfferCategoryRepositories
{
    public class OfferCategoryRepository : BaseRepository
    {
        public OfferCategoryRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public (ResponseResultType Result, string Message) Add(string name)
        {
            if (DbContext.OfferCategories.Any(oc => oc.Name == name))
                return (ResponseResultType.AlreadyExists, $"Offer category with name {name} already exists.");

            var offerCategory = new OfferCategory
            {
                Name = name
            };

            DbContext.OfferCategories.Add(offerCategory);
            var result = SaveChanges();

            return (result == ResponseResultType.Success)
                ? (result, "Offer category has been succesfully added.")
                : (result, "No changes made.");
        }

        public ResponseResultType Edit(OfferCategory offerCategory, int offerCategoryId)
        {
            var edittingOfferCategory = DbContext.OfferCategories.Find(offerCategoryId);

            if (edittingOfferCategory == null)
            {
                return ResponseResultType.NotFound;
            }

            edittingOfferCategory.Name = offerCategory.Name;

            return SaveChanges();
        }

        public ResponseResultType Delete(int offerCategoryId)
        {
            var offerCategory = DbContext.OfferCategories.Find(offerCategoryId);

            if (offerCategory == null)
                return ResponseResultType.NotFound;

            DbContext.OfferCategories.Remove(offerCategory);

            return SaveChanges();
        }

        public OfferCategory GetById(int id)
        {
            return DbContext.OfferCategories
                .Include(oc => oc.Offers)
                .Where(oc => oc.Id == id)
                .FirstOrDefault();
        }

        public ICollection<OfferCategory> GetAll()
        {
            return DbContext.OfferCategories
                .Include(oc => oc.Offers)
                .ToList();
        }
    }
}
