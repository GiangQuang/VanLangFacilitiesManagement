using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VLFM.Infrastructure.Migrations
{
    public partial class Migv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Propertycode = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    PropTypeID = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Propertyname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
