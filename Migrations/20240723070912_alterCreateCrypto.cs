using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finances.Migrations
{
    /// <inheritdoc />
    public partial class alterCreateCrypto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ScrapedDataYahooCrypto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Change = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeInProcentige = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolumeInCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolumeOutCurrency24Hr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalVolumeAllCurrencies24Hr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CirculatingSupply = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapedDataYahooCrypto", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "ScrapedDataYahooCrypto");

         
        }
    }
}
