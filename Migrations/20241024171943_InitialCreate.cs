using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostBasedCharges",
                columns: table => new
                {
                    CostChargeId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostBasedCharges", x => x.CostChargeId);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    CostId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.CostId);
                });

            migrationBuilder.CreateTable(
                name: "OrderBasedCharges",
                columns: table => new
                {
                    OrderChargeId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBasedCharges", x => x.OrderChargeId);
                });

            migrationBuilder.CreateTable(
                name: "PlatformOrders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformOrders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "BillingAccounts",
                columns: table => new
                {
                    BillingAccountId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AccountBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAccounts", x => x.BillingAccountId);
                    table.ForeignKey(
                        name: "FK_BillingAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreightOutbounds",
                columns: table => new
                {
                    OutboundOrderId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderShipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecipientPostCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DestinationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingCompany = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransportDays = table.Column<int>(type: "int", nullable: false),
                    RelatedAdjustmentOrder = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReferenceOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OutboundMethod = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightOutbounds", x => x.OutboundOrderId);
                    table.ForeignKey(
                        name: "FK_FreightOutbounds_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FreightOutbounds_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InboundOrders",
                columns: table => new
                {
                    InboundOrderId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EstimatedArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Boxes = table.Column<int>(type: "int", nullable: false),
                    InboundType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReferenceOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ArrivalMethod = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundOrders", x => x.InboundOrderId);
                    table.ForeignKey(
                        name: "FK_InboundOrders_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InboundOrders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Inventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParcelOutbounds",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransportDays = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ReferenceOrderNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Boxes = table.Column<int>(type: "int", nullable: false),
                    ShippingCompany = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LatestInformation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TrackingUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternetPostingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatedAdjustmentOrder = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelOutbounds", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_ParcelOutbounds_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParcelOutbounds_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    BillingId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    BillingAccountId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.BillingId);
                    table.ForeignKey(
                        name: "FK_Billings_BillingAccounts_BillingAccountId",
                        column: x => x.BillingAccountId,
                        principalTable: "BillingAccounts",
                        principalColumn: "BillingAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostBasedBillings",
                columns: table => new
                {
                    BillingAccountId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CostChargeId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostBasedBillings", x => new { x.BillingAccountId, x.CostChargeId });
                    table.ForeignKey(
                        name: "FK_CostBasedBillings_BillingAccounts_BillingAccountId",
                        column: x => x.BillingAccountId,
                        principalTable: "BillingAccounts",
                        principalColumn: "BillingAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CostBasedBillings_CostBasedCharges_CostChargeId",
                        column: x => x.CostChargeId,
                        principalTable: "CostBasedCharges",
                        principalColumn: "CostChargeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderBasedBillings",
                columns: table => new
                {
                    BillingAccountId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OrderChargeId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBasedBillings", x => new { x.BillingAccountId, x.OrderChargeId });
                    table.ForeignKey(
                        name: "FK_OrderBasedBillings_BillingAccounts_BillingAccountId",
                        column: x => x.BillingAccountId,
                        principalTable: "BillingAccounts",
                        principalColumn: "BillingAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderBasedBillings_OrderBasedCharges_OrderChargeId",
                        column: x => x.OrderChargeId,
                        principalTable: "OrderBasedCharges",
                        principalColumn: "OrderChargeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FreightProductLists",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightProductLists", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_FreightProductLists_FreightOutbounds_OrderId",
                        column: x => x.OrderId,
                        principalTable: "FreightOutbounds",
                        principalColumn: "OutboundOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FreightProductLists_Inventories_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Inventories",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InboundProductLists",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundProductLists", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_InboundProductLists_InboundOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "InboundOrders",
                        principalColumn: "InboundOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InboundProductLists_Inventories_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Inventories",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Inventories_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Inventories",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParcelProductLists",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelProductLists", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ParcelProductLists_Inventories_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Inventories",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParcelProductLists_ParcelOutbounds_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ParcelOutbounds",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingAccounts_UserId",
                table: "BillingAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Billings_BillingAccountId",
                table: "Billings",
                column: "BillingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CostBasedBillings_CostChargeId",
                table: "CostBasedBillings",
                column: "CostChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_FreightOutbounds_CreatorId",
                table: "FreightOutbounds",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_FreightOutbounds_WarehouseId",
                table: "FreightOutbounds",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_FreightProductLists_ProductId",
                table: "FreightProductLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundOrders_CreatorId",
                table: "InboundOrders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundOrders_WarehouseId",
                table: "InboundOrders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundProductLists_ProductId",
                table: "InboundProductLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBasedBillings_OrderChargeId",
                table: "OrderBasedBillings",
                column: "OrderChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelOutbounds_CreatorId",
                table: "ParcelOutbounds",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelOutbounds_WarehouseId",
                table: "ParcelOutbounds",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelProductLists_ProductId",
                table: "ParcelProductLists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "CostBasedBillings");

            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "FreightProductLists");

            migrationBuilder.DropTable(
                name: "InboundProductLists");

            migrationBuilder.DropTable(
                name: "OrderBasedBillings");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ParcelProductLists");

            migrationBuilder.DropTable(
                name: "PlatformOrders");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "CostBasedCharges");

            migrationBuilder.DropTable(
                name: "FreightOutbounds");

            migrationBuilder.DropTable(
                name: "InboundOrders");

            migrationBuilder.DropTable(
                name: "BillingAccounts");

            migrationBuilder.DropTable(
                name: "OrderBasedCharges");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "ParcelOutbounds");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
