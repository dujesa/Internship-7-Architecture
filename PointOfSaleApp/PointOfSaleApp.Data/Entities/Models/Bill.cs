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
        public BillType BillType { get; set; }

        public int? OneOffBillId { get; set; }
        public OneOffBill OneOffBill { get; set; }

        public int? ServiceBillId { get; set; }
        public ServiceBill ServiceBill { get; set; }

        public int? SubscriptionBillId { get; set; }
        public SubscriptionBill SubscriptionBill { get; set; }

        public ICollection<BillItem> BillItems { get; set; }
    }
}
