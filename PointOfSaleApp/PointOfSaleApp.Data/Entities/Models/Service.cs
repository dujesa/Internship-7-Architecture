using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Service
    {
        public int Id { get; set; }
        public decimal PricePerHour { get; set; }
        public int WorkingHoursNeeded { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public Service()
        {
        }

        public Service(Offer offer)
        {
            OfferId = offer.Id;
            Offer = offer;
        }
    }
}
