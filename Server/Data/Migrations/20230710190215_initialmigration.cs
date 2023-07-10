using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catergories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ThumbnailImagePath = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catergories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "Catergories",
                columns: new[] { "CategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[,]
                {
                    { 1, "A Description of category [i]", "Category [i]", "uploads/placeholder.jpg" },
                    { 2, "A Description of category [i]", "Category [i]", "uploads/placeholder.jpg" },
                    { 3, "A Description of category [i]", "Category [i]", "uploads/placeholder.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catergories");
        }
    }
}
