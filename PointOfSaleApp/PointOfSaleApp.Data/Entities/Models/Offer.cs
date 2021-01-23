using PointOfSaleApp.Data.Enums;
using System.Collections.Generic;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public int SoldQuantity { get; set; } = 0;
        public OfferType OfferType { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public ICollection<OfferCategory> OfferCategories { get; set; }

        public Offer()
        {
            OfferCategories = new List<OfferCategory>();
        }
    }
}
