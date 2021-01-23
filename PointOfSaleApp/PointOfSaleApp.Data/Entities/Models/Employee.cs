using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginPassword { get; set; }
        public string PIN { get; set; }
        public int DailyWorkingHours { get; set; }
        public int ServiceHoursToDo { get; set; } = 0;

        public ICollection<ServiceBill> ServiceBills { get; set; }

        public Employee()
        {
        }
    }
}
