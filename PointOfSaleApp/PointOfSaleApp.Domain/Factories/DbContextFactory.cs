using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PointOfSaleApp.Domain.Factories
{
    public static class DbContextFactory
    {
        public static PointOfSaleDbContext GetPointOfSaleDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["PointOfSale"].ConnectionString)
                .Options;

            return new PointOfSaleDbContext(options);
        }
    }
}
