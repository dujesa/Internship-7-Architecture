using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class ServiceBill
    {
        public int Id { get; set; }
        public DateTime PickupTime { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ServiceBill()
        {
        }

        public ServiceBill(Bill bill)
        {
            Bill = bill;
        }
    }
}
