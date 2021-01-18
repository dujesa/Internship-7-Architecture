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

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
