using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkeyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateCompanyDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Companys",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Companys");
        }
    }
}
