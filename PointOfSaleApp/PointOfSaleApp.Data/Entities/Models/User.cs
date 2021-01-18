using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PIN { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
