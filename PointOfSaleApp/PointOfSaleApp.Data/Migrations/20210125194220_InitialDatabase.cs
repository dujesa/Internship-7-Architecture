using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSaleApp.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    BillType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyWorkingHours = table.Column<int>(type: "int", nullable: false),
                    ServiceHoursToDo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    SoldQuantity = table.Column<int>(type: "int", nullable: false),
                    OfferType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsTerminated = table.Column<bool>(type: "bit", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionBills_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionBills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneOffBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneOffBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneOffBills_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OneOffBills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBills_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItems_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferOfferCategory",
                columns: table => new
                {
                    OfferCategoriesId = table.Column<int>(type: "int", nullable: false),
                    OffersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferOfferCategory", x => new { x.OfferCategoriesId, x.OffersId });
                    table.ForeignKey(
                        name: "FK_OfferOfferCategory_OfferCategories_OfferCategoriesId",
                        column: x => x.OfferCategoriesId,
                        principalTable: "OfferCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferOfferCategory_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkingHoursNeeded = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "BillType", "IsCancelled", "IssuedAt", "Price" },
                values: new object[,]
                {
                    { 1, 0, false, new DateTime(2021, 1, 25, 20, 42, 17, 485, DateTimeKind.Local).AddTicks(7682), 199.99m },
                    { 2, 0, false, new DateTime(2021, 1, 25, 20, 42, 17, 498, DateTimeKind.Local).AddTicks(2968), 14.99m },
                    { 3, 1, false, new DateTime(2021, 1, 25, 20, 42, 17, 498, DateTimeKind.Local).AddTicks(3110), 99.99m },
                    { 4, 2, false, new DateTime(2021, 1, 25, 20, 42, 17, 498, DateTimeKind.Local).AddTicks(3135), 100.59m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreditCardNumber", "FirstName", "LastName", "PIN" },
                values: new object[] { 1, "01920123", "Mladen", "Mladenović", "12345" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DailyWorkingHours", "FirstName", "LastName", "LoginPassword", "PIN", "ServiceHoursToDo" },
                values: new object[] { 1, 8, "Pero", "Perić", "dumptest", "23456", 0 });

            migrationBuilder.InsertData(
                table: "OfferCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Božićne ponude" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "AvailableQuantity", "Description", "Name", "OfferType", "SoldQuantity" },
                values: new object[,]
                {
                    { 1, 20, "/", "Okvir karbonski - Giant", 0, 0 },
                    { 2, 4, "/", "Popravak kvarova u mjenaču", 1, 0 },
                    { 3, 2, "/", "Posudba MTB bicikala", 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "OfferId", "Price" },
                values: new object[] { 1, 1, 99.99m });

            migrationBuilder.InsertData(
                table: "BillItems",
                columns: new[] { "Id", "BillId", "OfferId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 1 },
                    { 3, 2, 2, 1 },
                    { 4, 3, 2, 4 },
                    { 5, 4, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "OneOffBills",
                columns: new[] { "Id", "BillId", "EmployeeId", "PickupTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2021, 1, 25, 22, 57, 17, 499, DateTimeKind.Local).AddTicks(2745) },
                    { 2, 2, 1, new DateTime(2021, 2, 4, 20, 42, 17, 499, DateTimeKind.Local).AddTicks(7064) }
                });

            migrationBuilder.InsertData(
                table: "ServiceBills",
                columns: new[] { "Id", "BillId", "EmployeeId", "PickupTime" },
                values: new object[] { 1, 3, 1, new DateTime(2021, 1, 25, 21, 27, 17, 500, DateTimeKind.Local).AddTicks(4921) });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "OfferId", "PricePerHour", "WorkingHoursNeeded" },
                values: new object[] { 1, 2, 9.99m, 2 });

            migrationBuilder.InsertData(
                table: "SubscriptionBills",
                columns: new[] { "Id", "BillId", "CustomerId", "EndTime", "IsTerminated" },
                values: new object[] { 1, 4, 1, new DateTime(2022, 1, 25, 20, 42, 17, 501, DateTimeKind.Local).AddTicks(8606), false });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "OfferId", "PricePerDay" },
                values: new object[] { 1, 3, 99.99m });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_OfferId",
                table: "Articles",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_OfferId",
                table: "BillItems",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferCategory_OffersId",
                table: "OfferOfferCategory",
                column: "OffersId");

            migrationBuilder.CreateIndex(
                name: "IX_OneOffBills_BillId",
                table: "OneOffBills",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_OneOffBills_EmployeeId",
                table: "OneOffBills",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBills_BillId",
                table: "ServiceBills",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBills_EmployeeId",
                table: "ServiceBills",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OfferId",
                table: "Services",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBills_BillId",
                table: "SubscriptionBills",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionBills_CustomerId",
                table: "SubscriptionBills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_OfferId",
                table: "Subscriptions",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "OfferOfferCategory");

            migrationBuilder.DropTable(
                name: "OneOffBills");

            migrationBuilder.DropTable(
                name: "ServiceBills");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SubscriptionBills");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "OfferCategories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Offers");
        }
    }
}
