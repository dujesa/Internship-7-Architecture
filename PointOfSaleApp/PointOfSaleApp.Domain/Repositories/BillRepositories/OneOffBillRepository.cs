using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.BillRepositories
{
    public class OneOffBillRepository : BaseRepository
    {
        private readonly BillRepository _billRepository;

        public OneOffBillRepository(PointOfSaleDbContext dbContext, BillRepository billRepository) : base(dbContext)
        {
            _billRepository = billRepository;
        }
        public ResponseResultType Add(Bill bill, DateTime pickupTime)
        {
            var oneOffBill = new OneOffBill
            {
                Bill = bill,
                PickupTime = pickupTime
            };

            DbContext.OneOffBills.Add(oneOffBill);

            return SaveChanges();
        }
    }
}
