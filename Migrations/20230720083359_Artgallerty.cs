using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGalleryOnline.Migrations
{
    public partial class Artgallerty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorArtWork",
                columns: table => new
                {
                    AuthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthDesciption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorArtWork", x => x.AuthId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotifiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotifiDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotifiId);
                });

            migrationBuilder.CreateTable(
                name: "shippingFee",
                columns: table => new
                {
                    ShipFeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingFee", x => x.ShipFeeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserGender = table.Column<int>(type: "int", nullable: false),
                    UserAge = table.Column<int>(type: "int", nullable: false),
                    UserPhoneNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ArtWork",
                columns: table => new
                {
                    ArtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArtDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArtImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuthId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtWork", x => x.ArtId);
                    table.ForeignKey(
                        name: "FK_ArtWork_AuthorArtWork_AuthId",
                        column: x => x.AuthId,
                        principalTable: "AuthorArtWork",
                        principalColumn: "AuthId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AuthId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blog_AuthorArtWork_AuthId",
                        column: x => x.AuthId,
                        principalTable: "AuthorArtWork",
                        principalColumn: "AuthId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                name: "UserNotification",
                columns: table => new
                {
                    UnotifiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotifiId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => x.UnotifiId);
                    table.ForeignKey(
                        name: "FK_UserNotification_Notification_NotifiId",
                        column: x => x.NotifiId,
                        principalTable: "Notification",
                        principalColumn: "NotifiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotification_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ArtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_Interest_ArtWork_ArtId",
                        column: x => x.ArtId,
                        principalTable: "ArtWork",
                        principalColumn: "ArtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ArtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_ArtWork_ArtId",
                        column: x => x.ArtId,
                        principalTable: "ArtWork",
                        principalColumn: "ArtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shippings",
                columns: table => new
                {
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingStatus = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippings", x => x.ShippingId);
                    table.ForeignKey(
                        name: "FK_shippings_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtWork_AuthId",
                table: "ArtWork",
                column: "AuthId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_AuthId",
                table: "Blog",
                column: "AuthId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_ArtId",
                table: "Interest",
                column: "ArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_UserId",
                table: "Interest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ArtId",
                table: "OrderDetail",
                column: "ArtId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_OrderId",
                table: "payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_shippings_OrderId",
                table: "shippings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_NotifiId",
                table: "UserNotification",
                column: "NotifiId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotification_UserId",
                table: "UserNotification",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "shippingFee");

            migrationBuilder.DropTable(
                name: "shippings");

            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropTable(
                name: "ArtWork");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "AuthorArtWork");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
