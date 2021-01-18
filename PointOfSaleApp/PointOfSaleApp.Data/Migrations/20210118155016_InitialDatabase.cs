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
                    BillType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
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
                    OfferType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OneOffBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyWorkingHours = table.Column<int>(type: "int", nullable: false),
                    ServiceHoursToDo = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "BillType", "IssuedAt", "Price" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2021, 1, 18, 16, 50, 15, 758, DateTimeKind.Local).AddTicks(866), 199.99m },
                    { 2, 0, new DateTime(2021, 1, 18, 16, 50, 15, 761, DateTimeKind.Local).AddTicks(9166), 14.99m },
                    { 3, 1, new DateTime(2021, 1, 18, 16, 50, 15, 761, DateTimeKind.Local).AddTicks(9216), 99.99m },
                    { 4, 2, new DateTime(2021, 1, 18, 16, 50, 15, 761, DateTimeKind.Local).AddTicks(9222), 100.59m }
                });

            migrationBuilder.InsertData(
                table: "OfferCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Božićne ponude" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "AvailableQuantity", "Description", "Name", "OfferType" },
                values: new object[,]
                {
                    { 1, 20, "/", "Okvir karbonski - Giant", 0 },
                    { 2, 4, "/", "Popravak kvarova u mjenaču", 1 },
                    { 3, 2, "/", "Posudba MTB bicikala", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PIN" },
                values: new object[,]
                {
                    { 1, "Mladen", "Mladenović", "12345" },
                    { 2, "Pero", "Perić", "23456" }
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
                table: "Customers",
                columns: new[] { "Id", "CreditCardNumber", "UserId" },
                values: new object[] { 1, "01920123", 1 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DailyWorkingHours", "ServiceHoursToDo", "UserId" },
                values: new object[] { 1, 8, 0, 2 });

            migrationBuilder.InsertData(
                table: "OneOffBills",
                columns: new[] { "Id", "BillId", "PickupTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 1, 18, 19, 5, 15, 762, DateTimeKind.Local).AddTicks(2189) },
                    { 2, 2, new DateTime(2021, 1, 28, 16, 50, 15, 762, DateTimeKind.Local).AddTicks(3289) }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "OfferId", "PricePerHour", "WorkingHoursNeeded" },
                values: new object[] { 1, 2, 9.99m, 2 });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "OfferId", "PricePerDay" },
                values: new object[] { 1, 3, 99.99m });

            migrationBuilder.InsertData(
                table: "ServiceBills",
                columns: new[] { "Id", "BillId", "EmployeeId", "PickupTime" },
                values: new object[] { 1, 3, 1, new DateTime(2021, 1, 18, 17, 35, 15, 762, DateTimeKind.Local).AddTicks(5856) });

            migrationBuilder.InsertData(
                table: "SubscriptionBills",
                columns: new[] { "Id", "BillId", "CustomerId", "IsCancelled" },
                values: new object[] { 1, 4, 1, false });

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
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferOfferCategory_OffersId",
                table: "OfferOfferCategory",
                column: "OffersId");

            migrationBuilder.CreateIndex(
                name: "IX_OneOffBills_BillId",
                table: "OneOffBills",
                column: "BillId");

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

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
