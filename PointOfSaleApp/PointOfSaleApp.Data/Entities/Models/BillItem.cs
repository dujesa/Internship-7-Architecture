using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class BillItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
