using PointOfSaleApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public OfferType OfferType { get; set; }

        public int? ArticleId { get; set; }
        public Article Article { get; set; }

        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        public int? SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public /*virtual*/ ICollection<OfferCategory> OfferCategories { get; set; }
    }
}
