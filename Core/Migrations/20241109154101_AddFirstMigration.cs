using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneIMEI = table.Column<long>(type: "bigint", nullable: false),
                    PhoneIsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    PhoneBattery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneChip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExisting = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
