using Microsoft.EntityFrameworkCore.Migrations;

namespace ImproveTeam.Infrastructure.DataAccess.EF.Migrations
{
    public partial class CreateAdvertiserProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertiserProducts",
                columns: table => new
                {
                    AdvertiserId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiserProducts", x => new { x.AdvertiserId, x.ProductId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertiserProducts");
        }
    }
}
