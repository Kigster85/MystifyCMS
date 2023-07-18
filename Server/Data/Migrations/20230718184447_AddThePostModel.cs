using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThePostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Catergories",
                table: "Catergories");

            migrationBuilder.RenameTable(
                name: "Catergories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Excerpt = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 65536, nullable: false),
                    PublishDate = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Description of category 1", "Category 1" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Description of category 2", "Category 2" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Description of category 3", "Category 3" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "CategoryId", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title" },
                values: new object[,]
                {
                    { 1, "Philip Billson", 1, "", "This is the excerpt for post 1. As excerpt is a little extraction from a larger piece of text. Sort of like a preview.", "44/18/2023 06:44", true, "uploads/placeholder.png", "First Post" },
                    { 2, "Philip Billson", 2, "", "This is the excerpt for post 2. As excerpt is a little extraction from a larger piece of text. Sort of like a preview.", "44/18/2023 06:44", true, "uploads/placeholder.png", "Second Post" },
                    { 3, "Philip Billson", 3, "", "This is the excerpt for post 3. As excerpt is a little extraction from a larger piece of text. Sort of like a preview.", "44/18/2023 06:44", true, "uploads/placeholder.png", "Third Post" },
                    { 4, "Philip Billson", 1, "", "This is the excerpt for post 4. As excerpt is a little extraction from a larger piece of text. Sort of like a preview.", "44/18/2023 06:44", true, "uploads/placeholder.png", "Fourth Post" },
                    { 5, "Philip Billson", 2, "", "This is the excerpt for post 5. As excerpt is a little extraction from a larger piece of text. Sort of like a preview.", "44/18/2023 06:44", true, "uploads/placeholder.png", "Fifth Post" },
                    { 6, "Philip Billson", 3, "", "This is the excerpt for post 6. As excerpt is a little extraction from a larger piece of text. Sort of like a preview.", "44/18/2023 06:44", true, "uploads/placeholder.png", "Sixth Post" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Catergories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catergories",
                table: "Catergories",
                column: "CategoryId");

            migrationBuilder.UpdateData(
                table: "Catergories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Description of category [i]", "Category [i]" });

            migrationBuilder.UpdateData(
                table: "Catergories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Description of category [i]", "Category [i]" });

            migrationBuilder.UpdateData(
                table: "Catergories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "A Description of category [i]", "Category [i]" });
        }
    }
}
