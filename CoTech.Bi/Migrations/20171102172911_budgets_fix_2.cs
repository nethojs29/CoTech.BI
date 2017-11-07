using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class budgets_fix_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExpenseTypeId",
                table: "Budget_Concepts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_ExpenseTypeId",
                table: "Budget_Concepts",
                column: "ExpenseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_ExpenseTypes_ExpenseTypeId",
                table: "Budget_Concepts",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Concepts_ExpenseTypes_ExpenseTypeId",
                table: "Budget_Concepts");

            migrationBuilder.DropIndex(
                name: "IX_Budget_Concepts_ExpenseTypeId",
                table: "Budget_Concepts");

            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "Budget_Concepts");
        }
    }
}
