using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class SubscriptionBill
    {
        public int Id { get; set; }
        public bool IsCancelled { get; set; } = false;

        public int BillId { get; set; }
        public Bill Bill { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public SubscriptionBill()
        {
        }

        public SubscriptionBill(Bill bill)
        {
            Bill = bill;
        }
    }
}
