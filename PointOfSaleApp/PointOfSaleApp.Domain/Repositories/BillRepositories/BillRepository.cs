using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
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
        public (ResponseResultType Result, string Message) CreateNew()
        {

            var bill = new Bill
            {
                IssuedAt = DateTime.Now,
                Price = 0
            };

            DbContext.Bills.Add(bill);
            var result = SaveChanges();

            return (result == ResponseResultType.Success) 
                ? (result, "Bill has been successfully issued.") 
                : (result, "No changes made, no new offer added.");
        }
        /*if (bill.Price < 0)
    return (ResponseResultType.ValidationError, "Error related to bill price, cannot be price lower than zero!");
*/
        /*if (bill.BillItems.Count == 0)
            return (ResponseResultType.ValidationError, "Error, bill must contain at least one item on it to be issued!");
        */
        /*public ICollection<Offer> GetIssuedInside()
        {
            return DbContext.Offers
                .Where(o => o.AvailableQuantity <= 0)
                .ToList();
        }*/

        /*public ResponseResultType AddBillItem(Offer offer, int billId)
        {
            var bill = GetById(billId);


            if (HasBillItem(offer.Id, billId))


            return SaveChanges();
        }*/

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

            return SaveChanges();
        }

        public ICollection<Bill> GetAll()
        {

            return DbContext.Bills
                .ToList();
        }
    }
}
