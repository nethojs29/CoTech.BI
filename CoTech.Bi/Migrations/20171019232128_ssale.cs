using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class ssale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SaleId",
                table: "Daily_Service_Sale",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Service_Sales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Total = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Sales_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Service_Sales_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_SaleId",
                table: "Daily_Service_Sale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Sales_CompanyId",
                table: "Service_Sales",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Sales_CreatorId",
                table: "Service_Sales",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Service_Sales_SaleId",
                table: "Daily_Service_Sale",
                column: "SaleId",
                principalTable: "Service_Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Daily_Service_Sale_Service_Sales_SaleId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropTable(
                name: "Service_Sales");

            migrationBuilder.DropIndex(
                name: "IX_Daily_Service_Sale_SaleId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Daily_Service_Sale");
        }
    }
}
