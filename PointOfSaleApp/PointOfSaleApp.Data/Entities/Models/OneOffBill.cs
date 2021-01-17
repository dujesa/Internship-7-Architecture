using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class OneOffBill
    {
        public int Id { get; set; }
        public DateTime PickupTime { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }

        public OneOffBill()
        {
        }

        public OneOffBill(Bill bill)
        {
            Bill = bill;
        }
    }
}
