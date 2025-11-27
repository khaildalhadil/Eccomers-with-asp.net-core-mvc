using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkeyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companys_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ListPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price100",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price50",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Jewelry");

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Bags");

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Shoes");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { "Premium handcrafted leather wallet with multiple card slots and a slim, elegant design.", "https://images-na.ssl-images-amazon.com/images/I/61BygHH-M2L.jpg", 25.0, "Leather Wallet" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { "Minimalist stainless-steel wristwatch with waterproof build and adjustable strap.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTO4mkuWYl3Od8zCp371ZqsWRdMZ_67G5DrXA&s", 120.0, "Classic Wristwatch" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 1, "UV400-protected sunglasses with lightweight frame and modern style.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSH1uw5ScCjhOOkas_LI2yn2Z6ef-n04Z8V_g&s", 35.0, "Sunglasses" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Title" },
                values: new object[] { 1, "Durable water-resistant backpack with 3 compartments and laptop pocket.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRcRk7MJ8S4wwT_4SYkSt5uNgHWhr_u6wo0dQ&s", "Travel Backpack" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 1, "Noise-isolating Bluetooth earbuds with fast charging and 8-hour battery life.", "https://m.media-amazon.com/images/I/51GEDaBzrwL.jpg", 55.0, "Wireless Earbuds" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ListPrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price100",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price50",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Categorys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 10, "Action" });

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 11, "SciFi" });

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 12, "History" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "Billy Spark", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SWD9999001", "", 99.0, 90.0, 80.0, 85.0, "Fortune of Time" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "Nancy Hoover", "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "CAW777777701", "", 40.0, 30.0, 20.0, 25.0, "Dark Skies" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "Julian Button", 3, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "RITO5555501", "", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price100", "Price50", "Title" },
                values: new object[] { "Abby Muscles", 2, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "WS3333333301", "", 70.0, 55.0, 60.0, "Cotton Candy" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "Ron Parker", 3, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SOTJ1111111101", "", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { 6, "Laura Phantom", 1, "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "FOT000000001", "", 25.0, 23.0, 20.0, 22.0, "Leaves and Wonders" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companys_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "Id");
        }
    }
}
