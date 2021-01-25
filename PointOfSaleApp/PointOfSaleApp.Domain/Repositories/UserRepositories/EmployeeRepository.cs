using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.UserRepositories
{
    public class EmployeeRepository : BaseRepository
    {
        public EmployeeRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public Employee GetByPassword(string inputPassword)
        {
            return DbContext.Employees
                .Where(e => e.LoginPassword == inputPassword)
                .FirstOrDefault();
        }

        public ResponseResultType AddServiceHoursById(int id, int toDoHours)
        {
            var employee = DbContext.Employees
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (employee is Employee)
                employee.ServiceHoursToDo += toDoHours;

            return SaveChanges();
        }
    }
}
