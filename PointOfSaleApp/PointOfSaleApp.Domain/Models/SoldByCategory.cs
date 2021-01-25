using PointOfSaleApp.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Domain.Models
{
    public class SoldByCategory
    {
        public OfferCategory OfferCategory { get; set; }
        public int Count { get; set; }
    }
}
