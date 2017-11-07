using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class conceptsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExpenseGroupId",
                table: "Budget_Concepts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_ExpenseGroupId",
                table: "Budget_Concepts",
                column: "ExpenseGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_ExpensesGroups_ExpenseGroupId",
                table: "Budget_Concepts",
                column: "ExpenseGroupId",
                principalTable: "ExpensesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Concepts_ExpensesGroups_ExpenseGroupId",
                table: "Budget_Concepts");

            migrationBuilder.DropIndex(
                name: "IX_Budget_Concepts_ExpenseGroupId",
                table: "Budget_Concepts");

            migrationBuilder.DropColumn(
                name: "ExpenseGroupId",
                table: "Budget_Concepts");
        }
    }
}
