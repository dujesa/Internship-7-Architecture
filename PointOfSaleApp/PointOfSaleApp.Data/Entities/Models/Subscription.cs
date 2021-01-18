using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public decimal PricePerDay { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public Subscription()
        {
        }

        public Subscription(Offer offer)
        {
            SubscriptionOfferId = offer.Id;
            SubscriptionOffer = offer;
        }
    }
}
