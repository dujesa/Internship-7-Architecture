using PointOfSaleApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime IssuedAt { get; set; }
        public decimal Price { get; set; }
        public bool IsCancelled { get; set; } = false;
        public BillType BillType { get; set; }

        public ICollection<OneOffBill> OneOffBills { get; set; }
        public ICollection<ServiceBill> ServiceBills { get; set; }
        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }

        public ICollection<BillItem> BillItems { get; set; }
    }
}
