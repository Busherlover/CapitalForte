using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullCityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
       name: "City",
       table: "AspNetUsers",
       nullable: true,  // Allow NULL values
       oldClrType: typeof(string),
       oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
      name: "City",
      table: "AspNetUsers",
      type: "nvarchar(max)",
      nullable: false,  // Disallow NULL values
      oldClrType: typeof(string),
      oldNullable: true);
        }
    }
}
