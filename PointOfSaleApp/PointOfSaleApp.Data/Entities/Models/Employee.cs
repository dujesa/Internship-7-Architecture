using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int DailyWorkingHours { get; set; }
        public int ServiceHoursToDo { get; set; } = 0;

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<ServiceBill> ServiceBills { get; set; }

        public Employee()
        {
        }

        public Employee(User user)
        {
            UserId = user.Id;
            User = user;
        }
    }
}
