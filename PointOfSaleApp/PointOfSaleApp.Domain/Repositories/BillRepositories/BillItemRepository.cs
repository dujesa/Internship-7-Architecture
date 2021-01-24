using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.BillRepositories
{
    public class BillItemRepository : BaseRepository
    {
        public BillItemRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public (ResponseResultType Response, BillItem BillItem) Add(int billId, int offerId)
        {
            var billItem = new BillItem
            {
                BillId = billId,
                OfferId = offerId,
                Quantity = 0
            };

            DbContext.BillItems.Add(billItem);
            
            return (SaveChanges(), billItem);
        }

        public BillItem GetById(int itemId)
        {
            return DbContext.BillItems
                .Where(bi => bi.Id == itemId)
                .FirstOrDefault();
        }

        public BillItem GetByOfferIdAndBillId(int offerId, int billId)
        {
            return DbContext.BillItems
                .Where(bi => bi.OfferId == offerId)
                .Where(bi => bi.BillId == billId)
                .FirstOrDefault();
        }

        public ResponseResultType IncreaseQuantityByIdFor(int id, int additionalQuantity)
        {
            var billItem = GetById(id);

            if (billItem == null)
                return ResponseResultType.NotFound;

            billItem.Quantity += additionalQuantity;

            return SaveChanges();
        }
    }
}
