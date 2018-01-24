using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class facturasfixesvarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requisitions_Banks_BankId",
                table: "Requisitions");

            migrationBuilder.DropIndex(
                name: "IX_Requisitions_BankId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Requisitions");

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "SmallBox",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DinningRoomId",
                table: "Budgets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndPeriodDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InvoiceCode = table.Column<string>(type: "longtext", nullable: true),
                    Observations = table.Column<string>(type: "longtext", nullable: true),
                    PaidAmount = table.Column<double>(type: "double", nullable: false),
                    StartPeriodDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Folio = table.Column<string>(type: "longtext", nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    Observations = table.Column<string>(type: "longtext", nullable: true),
                    Payment = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Payments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Payments_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_BankId",
                table: "SmallBox",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DinningRoomId",
                table: "Budgets",
                column: "DinningRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Payments_CompanyId",
                table: "Invoice_Payments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Payments_CreatorId",
                table: "Invoice_Payments",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Payments_InvoiceId",
                table: "Invoice_Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BankId",
                table: "Invoices",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                table: "Invoices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CreatorId",
                table: "Invoices",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_DinningRooms_DinningRoomId",
                table: "Budgets",
                column: "DinningRoomId",
                principalTable: "DinningRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Banks_BankId",
                table: "SmallBox",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_DinningRooms_DinningRoomId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_SmallBox_Banks_BankId",
                table: "SmallBox");

            migrationBuilder.DropTable(
                name: "Invoice_Payments");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_SmallBox_BankId",
                table: "SmallBox");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_DinningRoomId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "SmallBox");

            migrationBuilder.DropColumn(
                name: "DinningRoomId",
                table: "Budgets");

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_BankId",
                table: "Requisitions",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Banks_BankId",
                table: "Requisitions",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
