using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }

        public Customer()
        {
        }

        public Customer(User user)
        {
            UserId = user.Id;
            User = user;
        }
    }
}
