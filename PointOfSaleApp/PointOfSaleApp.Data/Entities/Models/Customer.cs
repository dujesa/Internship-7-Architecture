using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PIN { get; set; }
        public string CreditCardNumber { get; set; }

        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }

        public Customer()
        {
        }
    }
}
