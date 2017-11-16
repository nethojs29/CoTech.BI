using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class siac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_ExpensesGroups_ExpenseGroupId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Providers_ProviderId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId1",
                table: "ExpensesGroups");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesGroups_TypeId1",
                table: "ExpensesGroups");

            migrationBuilder.DropIndex(
                name: "IX_Daily_Service_Sale_ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ExpenseGroupId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "TypeId1",
                table: "ExpensesGroups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Daily_Service_Sale");

            migrationBuilder.DropColumn(
                name: "ExpenseGroupId",
                table: "Budgets");

            migrationBuilder.AddColumn<long>(
                name: "RequisitionId",
                table: "SmallBox",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DinningRoomId",
                table: "Requisitions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "Requisitions",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "Requisitions",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Movements",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Iva",
                table: "Movements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                table: "ExpensesGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "ProviderId",
                table: "Expenses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SaleId",
                table: "Daily_Service_Sale",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DinningRoomId",
                table: "Clients",
                type: "bigint",
                nullable: true);

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
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpenseGroupId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseTypeId = table.Column<long>(type: "bigint", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Budget_Concepts_ExpensesGroups_ExpenseGroupId",
                        column: x => x.ExpenseGroupId,
                        principalTable: "ExpensesGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budget_Concepts_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    UserId = table.Column<long>(type: "bigint", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service_Sales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_Service_Sales_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_SmallBox_RequisitionId",
                table: "SmallBox",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_DinningRoomId",
                table: "Requisitions",
                column: "DinningRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_TypeId",
                table: "ExpensesGroups",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_SaleId",
                table: "Daily_Service_Sale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DinningRoomId",
                table: "Clients",
                column: "DinningRoomId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_ExpenseGroupId",
                table: "Budget_Concepts",
                column: "ExpenseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_Concepts_ExpenseTypeId",
                table: "Budget_Concepts",
                column: "ExpenseTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Service_Sales_ClientId",
                table: "Service_Sales",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Sales_CompanyId",
                table: "Service_Sales",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Sales_CreatorId",
                table: "Service_Sales",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_ExpenseTypes_ExpenseTypeId",
                table: "Budgets",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_DinningRooms_DinningRoomId",
                table: "Clients",
                column: "DinningRoomId",
                principalTable: "DinningRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Service_Sales_SaleId",
                table: "Daily_Service_Sale",
                column: "SaleId",
                principalTable: "Service_Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Providers_ProviderId",
                table: "Expenses",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId",
                table: "ExpensesGroups",
                column: "TypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_DinningRooms_DinningRoomId",
                table: "Requisitions",
                column: "DinningRoomId",
                principalTable: "DinningRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Requisitions_RequisitionId",
                table: "SmallBox",
                column: "RequisitionId",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_ExpenseTypes_ExpenseTypeId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_DinningRooms_DinningRoomId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Daily_Service_Sale_Service_Sales_SaleId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Providers_ProviderId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesGroups_ExpenseTypes_TypeId",
                table: "ExpensesGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisitions_DinningRooms_DinningRoomId",
                table: "Requisitions");

            migrationBuilder.DropForeignKey(
                name: "FK_SmallBox_Requisitions_RequisitionId",
                table: "SmallBox");

            migrationBuilder.DropTable(
                name: "Budget_Concepts");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Service_Sales");

            migrationBuilder.DropIndex(
                name: "IX_SmallBox_RequisitionId",
                table: "SmallBox");

            migrationBuilder.DropIndex(
                name: "IX_Requisitions_DinningRoomId",
                table: "Requisitions");

            migrationBuilder.DropIndex(
                name: "IX_ExpensesGroups_TypeId",
                table: "ExpensesGroups");

            migrationBuilder.DropIndex(
                name: "IX_Daily_Service_Sale_SaleId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropIndex(
                name: "IX_Clients_DinningRoomId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_ExpenseTypeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "RequisitionId",
                table: "SmallBox");

            migrationBuilder.DropColumn(
                name: "DinningRoomId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Iva",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropColumn(
                name: "DinningRoomId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "Budgets");

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

            migrationBuilder.AlterColumn<long>(
                name: "ProviderId",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Expenses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Daily_Service_Sale",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Daily_Service_Sale",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ExpenseGroupId",
                table: "Budgets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_TypeId1",
                table: "ExpensesGroups",
                column: "TypeId1");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Providers_ProviderId",
                table: "Expenses",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
