using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Service.API.Migrations
{
    public partial class DBInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    InventoryStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.CorrelationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
