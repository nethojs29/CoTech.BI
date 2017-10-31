using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class budget_concepts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_ExpensesGroups_ExpenseGroupId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropIndex(
                name: "IX_Daily_Service_Sale_ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ExpenseGroupId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropColumn(
                name: "ExpenseGroupId",
                table: "Budgets");

            migrationBuilder.AddColumn<long>(
                name: "ExpenseTypeId",
                table: "Budgets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Budget_Concepts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    BudgetId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget_Concepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_Concepts_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budget_Concepts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budget_Concepts_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ExpenseTypeId",
                table: "Budgets",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_BudgetId",
                table: "Budget_Concepts",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_CompanyId",
                table: "Budget_Concepts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_CreatorId",
                table: "Budget_Concepts",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_ExpenseTypes_ExpenseTypeId",
                table: "Budgets",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_ExpenseTypes_ExpenseTypeId",
                table: "Budgets");

            migrationBuilder.DropTable(
                name: "Budget_Concepts");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ExpenseTypeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "Budgets");

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Daily_Service_Sale",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExpenseGroupId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_ClientId",
                table: "Daily_Service_Sale",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ExpenseGroupId",
                table: "Budgets",
                column: "ExpenseGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_ExpensesGroups_ExpenseGroupId",
                table: "Budgets",
                column: "ExpenseGroupId",
                principalTable: "ExpensesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
