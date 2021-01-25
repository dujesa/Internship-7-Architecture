using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.BillRepositories
{
    public class ServiceBillRepository : BaseRepository
    {
        public ServiceBillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
        public ResponseResultType Add(Bill bill, DateTime pickupTime, Employee employee)
        {
            var serviceBill = new ServiceBill
            {
                Employee = employee,
                Bill = bill,
                PickupTime = pickupTime
            };

            DbContext.ServiceBills.Add(serviceBill);

            return SaveChanges();
        }
    }
}