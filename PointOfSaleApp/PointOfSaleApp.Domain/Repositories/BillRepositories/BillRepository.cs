using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.BillRepositories
{
    public class BillRepository : BaseRepository
    {
        public BillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public (ResponseResultType Response, Bill Bill) CreateNew()
        {
            var bill = new Bill
            {
                IssuedAt = DateTime.Now,
                Price = 0
            };

            DbContext.Bills.Add(bill);
            
            var response = SaveChanges();

            return (response, bill);

        }

        public decimal SumProfitByYear(int year)
        {
            return DbContext.Bills
                .Where(b => b.IssuedAt.Year == year)
                .Select(b => b.Price)
                .Sum();
        }

        public Bill GetById(int id)
        {
            return DbContext.Bills.Find(id);
        }

        public ResponseResultType CancelById(int billId)
        {
            var cancelingBill = GetById(billId);

            if (cancelingBill == null)
                return ResponseResultType.NotFound;

            cancelingBill.IsCancelled = true;
            cancelingBill.BillType = BillType.Undisclosed;

            return SaveChanges();
        }

        public ICollection<Bill> GetAllIssuedInsideByOfferCategoryIds(DateTime? startTime, DateTime? endTime, ICollection<int> categoryIds)
        {
            var items = DbContext.BillItems
                .Include(bi => bi.Bill)
                .Include(bi => bi.Offer)
                .ThenInclude(o => o.OfferCategories)
                .ToList();

            if (startTime is DateTime)
                items = items.Where(bi => bi.Bill.IssuedAt >= startTime).ToList();

            if (endTime is DateTime)
                items = items.Where(bi => bi.Bill.IssuedAt <= endTime).ToList();

            if (categoryIds.Count > 0)
                items = items.Where(i => i.Offer.OfferCategories.Any(cat => categoryIds.Any(y => y == cat.Id))).ToList();

            return items
                .Select(i => i.Bill).Distinct().ToList();
        }

        public ResponseResultType Edit(Bill bill, int id)
        {
            var edittingBill = DbContext.Bills.Find(id);

            if (edittingBill == null)
            {
                return ResponseResultType.NotFound;
            }

            edittingBill.BillType = bill.BillType;
            edittingBill.IsCancelled = bill.IsCancelled;
            edittingBill.Price = bill.Price;

            edittingBill.ServiceBills = bill.ServiceBills;
            edittingBill.SubscriptionBills = bill.SubscriptionBills;
            edittingBill.OneOffBills = bill.OneOffBills;

            return SaveChanges();
        }

        public ICollection<OfferType> GetContainedOfferTypesById(int id)
        {
            return DbContext.BillItems
                .Include(bi => bi.Bill)
                .Include(bi => bi.Offer)
                .Where(b => b.BillId == id)
                .GroupBy(bi => new
                {
                    bi.Offer.OfferType
                })
                .Select(bi => bi.Key.OfferType)
                .ToList();
        }

        public DateTime? GetPickupTimeById(int id)
        {
            var bill = DbContext.Bills
                .Include(b => b.OneOffBills)
                .Include(b => b.ServiceBills)
                .Where(b => b.Id == id)
                .FirstOrDefault();

            if (bill == null)
                return null;

            var oneOffBill = bill.OneOffBills.FirstOrDefault();
            var serviceBill = bill.ServiceBills.FirstOrDefault();

            if (bill.BillType == BillType.OneOffBill && oneOffBill is OneOffBill)
                return oneOffBill.PickupTime;

            if (bill.BillType == BillType.ServiceBill && serviceBill is ServiceBill)
                return serviceBill.PickupTime;

            return null;
        }

        public ICollection<Bill> GetAll()
        {

            return DbContext.Bills
                .ToList();
        }

        public void CloseById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
