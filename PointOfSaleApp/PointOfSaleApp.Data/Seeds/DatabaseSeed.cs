using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.Data.Entities.Models;
using PointOfSaleApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSaleApp.Data.Seeds
{
    public static class DatabaseSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasData(new List<Customer>
                { 
                    new Customer
                    {
                        Id = 1,
                        PIN = "12345",
                        FirstName = "Mladen",
                        LastName = "Mladenović",
                        CreditCardNumber = "01920123"
                    }
                });

            modelBuilder.Entity<Employee>()
                .HasData(new List<Employee>
                {
                    new Employee
                    {
                        Id = 1,
                        PIN = "23456",
                        FirstName = "Pero",
                        LastName = "Perić",
                        LoginPassword = "dumptest",
                        DailyWorkingHours = 8,
                    },
                    new Employee
                    {
                        Id = 2,
                        PIN = "74832",
                        FirstName = "Stipe",
                        LastName = "Stipic",
                        LoginPassword = "test2",
                        DailyWorkingHours = 8,
                    }
                });


            modelBuilder.Entity<OfferCategory>()
                .HasData(new List<OfferCategory>
                {
                    new OfferCategory
                    {
                        Id = 1,
                        Name = "Božićne ponude"
                    }
                });

            modelBuilder.Entity<Offer>()
                .HasData(new List<Offer>
                {
                    new Offer
                    {
                        Id = 1,
                        Name = "Okvir karbonski - Giant",
                        Description = "/",
                        AvailableQuantity = 20,
                        OfferType = OfferType.Article
                    },
                    new Offer
                    {
                        Id = 2,
                        Name = "Popravak kvarova u mjenaču",
                        Description = "/",
                        AvailableQuantity = 4,
                        OfferType = OfferType.Service
                    },
                    new Offer
                    {
                        Id = 3,
                        Name = "Posudba MTB bicikala",
                        Description = "/",
                        AvailableQuantity = 2,
                        OfferType = OfferType.Subscription
                    },
                });

             modelBuilder.Entity<Article>()
                .HasData(new List<Article>
                {
                    new Article
                    {
                        Id = 1,
                        OfferId = 1,
                        Price = 99.99m
                    }
                });

            modelBuilder.Entity<Service>()
               .HasData(new List<Service>
               {
                    new Service
                    {
                        Id = 1,
                        OfferId = 2,
                        PricePerHour = 9.99m,
                        WorkingHoursNeeded = 2
                    }
               });

            modelBuilder.Entity<Subscription>()
                .HasData(new List<Subscription>
                {
                    new Subscription
                    {
                        Id = 1,
                        PricePerDay = 99.99m,
                        OfferId = 3
                    }
                });

            modelBuilder.Entity<Bill>()
                .HasData(new List<Bill>
                {
                    new Bill
                    {
                        Id = 1,
                        BillType = BillType.OneOffBill,
                        IssuedAt = DateTime.Now,
                        Price = 199.99m
                    },
                    new Bill
                    {
                        Id = 2,
                        BillType = BillType.OneOffBill,
                        IssuedAt = DateTime.Now,
                        Price = 14.99m
                    },
                    new Bill
                    {
                        Id = 3,
                        BillType = BillType.ServiceBill,
                        IssuedAt = DateTime.Now,
                        Price = 99.99m
                    },
                    new Bill
                    {
                        Id = 4,
                        BillType = BillType.SubscriptionBill,
                        IssuedAt = DateTime.Now,
                        Price = 100.59m
                    },
                });

            modelBuilder.Entity<OneOffBill>()
                .HasData(new List<OneOffBill>
                {
                    new OneOffBill
                    {
                        Id = 1,
                        BillId = 1,
                        PickupTime = DateTime.Now.AddHours(2d).AddMinutes(15d),
                        EmployeeId = 1

                    },
                    new OneOffBill
                    {
                        Id = 2,
                        BillId = 2,
                        PickupTime = DateTime.Now.AddDays(10d),
                        EmployeeId = 1

                    }
                });

            modelBuilder.Entity<ServiceBill>()
                .HasData(new List<ServiceBill>
                {
                    new ServiceBill
                    {
                        Id = 1,
                        BillId = 3,
                        PickupTime = DateTime.Now.AddMinutes(45d),
                        EmployeeId = 1
                    }
                });
  
            modelBuilder.Entity<SubscriptionBill>()
                .HasData(new List<SubscriptionBill>
                {
                    new SubscriptionBill
                    {
                        Id = 1,
                        BillId = 4,
                        CustomerId = 1,
                        EndTime = DateTime.Now.AddYears(1)
                    }
                });
            
            modelBuilder.Entity<BillItem>()
                .HasData(new List<BillItem>
                {
                    new BillItem
                    {
                        Id = 1,
                        BillId = 1,
                        OfferId = 1,
                        Quantity = 2
                    }, 
                    new BillItem
                    {
                        Id = 2,
                        BillId = 2,
                        OfferId = 1,
                        Quantity = 1
                    },
                    new BillItem
                    {
                        Id = 3,
                        BillId = 2,
                        OfferId = 2,
                        Quantity = 1
                    },
                    new BillItem
                    {
                        Id = 4,
                        BillId = 3,
                        OfferId = 2,
                        Quantity = 4
                    },
                    new BillItem
                    {
                        Id = 5,
                        BillId = 4,
                        OfferId = 3,
                        Quantity = 1
                    }
                });
        }
    }
}
