using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Domain.Authentificators
{
    public static class AuthProvider
    {
        public static Employee GetEmployee(string inputPassword)
        {
            var employeeRepository = RepositoryFactory.GetRepository<EmployeeRepository>();
            
            return employeeRepository.GetByPassword(inputPassword);
        }
    }
}
