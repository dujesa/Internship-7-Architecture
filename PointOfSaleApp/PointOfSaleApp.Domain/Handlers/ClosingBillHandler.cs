using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using PointOfSaleApp.Domain.Enums;
using PointOfSaleApp.Domain.Factories;
using PointOfSaleApp.Domain.Repositories.BillRepositories;
using PointOfSaleApp.Domain.Repositories.OfferRepositories;
using PointOfSaleApp.Domain.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Domain.Handlers
{
    public static class ClosingBillHandler
    {
        public static EmployeeRepository employeeRepository = RepositoryFactory.GetRepository<EmployeeRepository>();
        public static BillRepository billRepository = RepositoryFactory.GetRepository<BillRepository>();
        public static BillItemRepository billItemRepository = RepositoryFactory.GetRepository<BillItemRepository>();
        public static OfferRepository offerRepository = RepositoryFactory.GetRepository<OfferRepository>();

        public static ResponseResultType Handle(Bill bill, Employee employee)
        {
            var billOfferTypes = billRepository.GetContainedOfferTypesById(bill.Id);

            if (billOfferTypes.Contains(OfferType.Article))
            {
                return HandleForArticleAndService(bill, employee);
            }
            else if (billOfferTypes.Contains(OfferType.Service))
            {
                return HandleForService(bill, employee);
            }

            return ResponseResultType.NoChanges;
        }

        public static ResponseResultType Handle(Bill bill, Customer customer, bool isMonthly)
        {
            var billOfferTypes = billRepository.GetContainedOfferTypesById(bill.Id);

            if (billOfferTypes.Contains(OfferType.Subscription))
            {
                return HandleForSubscription(bill, customer, isMonthly);
            }

            return ResponseResultType.NoChanges;
        }

        private static ResponseResultType HandleForArticleAndService(Bill bill, Employee employee)
        {
            var billItems = billItemRepository.GetAllByBillId(bill.Id);
            var workingHoursForBill = 0;

            bill.BillType = BillType.OneOffBill;

            foreach (var item in billItems)
            {
                var offer = offerRepository.FetchById(item.OfferId);

                if (offer.OfferType == OfferType.Service)
                {
                    var service = offer.Services.FirstOrDefault();

                    if (service == null)
                        return ResponseResultType.NotFound;

                    bill.Price += service.PricePerHour * service.WorkingHoursNeeded * item.Quantity;
                    workingHoursForBill += service.WorkingHoursNeeded * item.Quantity;
                }
                else if (offer.OfferType == OfferType.Article)
                {
                    var article = offer.Articles.FirstOrDefault();

                    if (article == null)
                        return ResponseResultType.NotFound;

                    bill.Price += article.Price * item.Quantity;
                }
            }

            var workingDays = workingHoursForBill / 8;
            var workingHours = workingHoursForBill % 8;

            var pickupTime = DateTime.Now.AddDays(workingDays).AddHours(workingHours);

            var employeeResponse = employeeRepository.AddServiceHoursById(employee.Id, workingHoursForBill);

            if (employeeResponse != ResponseResultType.Success)
                return employeeResponse;

            var oneOffBill = new OneOffBill
            {
                Employee = employee,
                Bill = bill,
                PickupTime = pickupTime
            };

            bill.OneOffBills.Add(oneOffBill);

            return billRepository.Edit(bill, bill.Id);
        }

        private static ResponseResultType HandleForService(Bill bill, Employee employee)
        {
            var billItems = billItemRepository.GetAllByBillId(bill.Id);
            var workingHoursForBill = 0;

            bill.BillType = BillType.ServiceBill;

            foreach (var item in billItems)
            {
                var offer = offerRepository.FetchById(item.OfferId);
                var service = offer.Services.FirstOrDefault();

                if (service == null)
                    return ResponseResultType.NotFound;

                bill.Price += service.PricePerHour * service.WorkingHoursNeeded * item.Quantity;
                workingHoursForBill += service.WorkingHoursNeeded * item.Quantity;
            }

            var workingDays = workingHoursForBill / 8;
            var workingHours = workingHoursForBill % 8;

            var pickupTime = DateTime.Now.AddDays(workingDays).AddHours(workingHours);
            
            var employeeResponse = employeeRepository.AddServiceHoursById(employee.Id, workingHoursForBill);

            if (employeeResponse != ResponseResultType.Success)
                return employeeResponse;
            
            var serviceBill = new ServiceBill
            {
                Employee = employee,
                Bill = bill,
                PickupTime = pickupTime
            };

            bill.ServiceBills.Add(serviceBill);

            return billRepository.Edit(bill, bill.Id);
        }

        private static ResponseResultType HandleForSubscription(Bill bill, Customer customer, bool isMonthly)
        {
            var billItems = billItemRepository.GetAllByBillId(bill.Id);
            var endTime = DateTime.Now;

            bill.BillType = BillType.SubscriptionBill;

            foreach (var item in billItems)
            {
                var offer = offerRepository.FetchById(item.OfferId);
                var subscription = offer.Subscriptions.FirstOrDefault();

                if (subscription == null)
                    return ResponseResultType.NotFound;


                if (isMonthly)
                    endTime = endTime.AddMonths(1);
                else
                    endTime = endTime.AddYears(1);

                bill.Price = subscription.PricePerDay * (decimal)(endTime - DateTime.Now).TotalDays;
            }

            var subscriptionBill = new SubscriptionBill
            {
                Customer = customer,
                Bill = bill,
                EndTime = endTime
            };

            bill.SubscriptionBills.Add(subscriptionBill);

            return billRepository.Edit(bill, bill.Id);
        }
    }
}
