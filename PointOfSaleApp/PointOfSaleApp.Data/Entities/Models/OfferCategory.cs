using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class OfferCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public OfferCategory()
        {
            Offers = new List<Offer>();
        }
    }
}
