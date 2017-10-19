using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId1",
                table: "ExpensesGroups");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesGroups_TypeId1",
                table: "ExpensesGroups");

            migrationBuilder.DropColumn(
                name: "TypeId1",
                table: "ExpensesGroups");

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                table: "ExpensesGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BirthPlace = table.Column<string>(type: "longtext", nullable: false),
                    CURP = table.Column<string>(type: "longtext", nullable: false),
                    Cellphone = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    CivilState = table.Column<string>(type: "longtext", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DailyIMSSSalary = table.Column<double>(type: "double", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    IMSSNumber = table.Column<string>(type: "longtext", nullable: false),
                    Infonavit = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    MonthlySalary = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    OtherContact = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    Position = table.Column<string>(type: "longtext", nullable: false),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false),
                    RFC = table.Column<string>(type: "longtext", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: false),
                    Suburb = table.Column<string>(type: "longtext", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personal_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personal_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personal_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personal_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_TypeId",
                table: "ExpensesGroups",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_CompanyId",
                table: "Personal",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_CreatorId",
                table: "Personal",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_DepartmentId",
                table: "Personal",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Personal_UserId",
                table: "Personal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId",
                table: "ExpensesGroups",
                column: "TypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId",
                table: "ExpensesGroups");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesGroups_TypeId",
                table: "ExpensesGroups");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "ExpensesGroups",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "TypeId1",
                table: "ExpensesGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_TypeId1",
                table: "ExpensesGroups",
                column: "TypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId1",
                table: "ExpensesGroups",
                column: "TypeId1",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
