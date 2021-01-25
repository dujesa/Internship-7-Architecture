using PointOfSaleApp.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Domain.Models
{
    public class ActiveSubscription
    {
        public Offer Offer { get; set; }
        public int Count { get; set; }
    }
}
