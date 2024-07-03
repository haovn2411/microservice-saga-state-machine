using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Service.API.Migrations
{
    public partial class DBInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "id", "name", "price", "unit" },
                values: new object[] { "0cdeaf69aa77441ab9e2db9a1ad411e7", "Iphone", 10f, 5 });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "id", "name", "price", "unit" },
                values: new object[] { "5e28916fdc774fd78a673f2a11765687", "Samsung", 10f, 5 });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "id", "name", "price", "unit" },
                values: new object[] { "6", "Sony", 10f, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
