using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Domain.Repositories.BillRepositories
{
    public class SubscriptionBillRepository : BaseRepository
    {
        public SubscriptionBillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Offer> GetAllActive()
        {
            return DbContext.SubscriptionBills
                .Include(sb => sb.Bill)
                .ThenInclude(b => b.BillItems)
                .ThenInclude(bi => bi.Offer)
                .Where(sb => sb.EndTime >= DateTime.Now && sb.IsTerminated == false)
                .Select(sb => sb.Bill.BillItems.First().Offer)
                .Distinct()
                .ToList();
            
        }
    }
}
