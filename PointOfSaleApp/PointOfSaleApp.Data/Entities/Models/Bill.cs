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
        public BillType BillType { get; set; } = BillType.Undisclosed;

        public ICollection<OneOffBill> OneOffBills { get; set; } = new List<OneOffBill>();
        public ICollection<ServiceBill> ServiceBills { get; set; } = new List<ServiceBill>();
        public ICollection<SubscriptionBill> SubscriptionBills { get; set; } = new List<SubscriptionBill>();

        public ICollection<BillItem> BillItems { get; set; } = new List<BillItem>();
    }
}
