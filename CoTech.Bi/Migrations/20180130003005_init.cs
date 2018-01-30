using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wer_Weeks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Weeks", x => x.Id);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "SmallBox",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Concept = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProviderId = table.Column<long>(type: "bigint", nullable: true),
                    RequisitionId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallBox", x => x.Id);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Movements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Concept = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Iva = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movements", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Services_Price_Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services_Price_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(type: "longtext", nullable: false),
                    Bank = table.Column<string>(type: "longtext", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    RFC = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DinningRoomId = table.Column<long>(type: "bigint", nullable: false),
                    ExpenseTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DinningRoomId = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    RFC = table.Column<string>(type: "longtext", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tradename = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company_Has_Modules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Has_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Daily_Service_Sale",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    iva = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daily_Service_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Daily_Service_Sale_Service_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Service_Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorEventId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DinningRooms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    City = table.Column<string>(type: "longtext", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    Responsable = table.Column<string>(type: "longtext", nullable: true),
                    State = table.Column<string>(type: "longtext", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinningRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpenseGroupId = table.Column<long>(type: "bigint", nullable: false),
                    Observations = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: false),
                    ProviderId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RequisitionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanyId1 = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanyId1 = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
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
                        name: "FK_Invoice_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lenders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    City = table.Column<string>(type: "longtext", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Increment = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    RFC = table.Column<string>(type: "longtext", nullable: true),
                    State = table.Column<string>(type: "longtext", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Suburb = table.Column<string>(type: "longtext", nullable: true),
                    postalCode = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatorEventId = table.Column<long>(type: "bigint", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
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
                        name: "FK_Personal_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Contact = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    RFC = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tradename = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requisitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ApproveUserId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    ComprobateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ComprobateFileUrl = table.Column<string>(type: "longtext", nullable: true),
                    ComprobateUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DinningRoomId = table.Column<long>(type: "bigint", nullable: false),
                    Keyword = table.Column<string>(type: "longtext", nullable: true),
                    LenderId = table.Column<long>(type: "bigint", nullable: true),
                    MotiveSurplus = table.Column<string>(type: "longtext", nullable: true),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: true),
                    Refund = table.Column<float>(type: "float", nullable: true),
                    ResponsableId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requisitions_DinningRooms_DinningRoomId",
                        column: x => x.DinningRoomId,
                        principalTable: "DinningRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requisitions_Lenders_LenderId",
                        column: x => x.LenderId,
                        principalTable: "Lenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requisitions_Files",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Extension = table.Column<string>(type: "longtext", nullable: true),
                    Mime = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    RequisitionId = table.Column<long>(type: "bigint", nullable: false),
                    Uri = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requisitions_Files_Requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wer_File_Company",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Extension = table.Column<string>(type: "longtext", nullable: true),
                    Mime = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Uri = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WeekId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_File_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_File_Company_Wer_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Wer_Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wer_Groups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wer_Reports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Financial = table.Column<string>(type: "longtext", nullable: true),
                    Observation = table.Column<string>(type: "longtext", nullable: true),
                    Operative = table.Column<string>(type: "longtext", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WeekId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_Reports_Wer_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Wer_Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wer_File",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Extension = table.Column<string>(type: "longtext", nullable: true),
                    Mime = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Uri = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_File_Wer_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Wer_Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Activity = table.Column<string>(type: "longtext", nullable: false),
                    Color = table.Column<string>(type: "longtext", nullable: true),
                    CreatorEventId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    PhotoUrl = table.Column<string>(type: "longtext", nullable: true),
                    Url = table.Column<string>(type: "varchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Companies_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatorEventId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "varchar(127)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Lastname = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RootUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RootUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wer_Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: true),
                    Tags = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WeekId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_Messages_Wer_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Wer_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_Messages_Wer_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Wer_Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wer_Party",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Createdat = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Party", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_Party_Wer_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Wer_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_Party_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wer_Seen_Reports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    SeenAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Seen_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_Seen_Reports_Wer_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Wer_Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_Seen_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wer_Token_User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Token_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_Token_User_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification_Receivers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification_Receivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Receivers_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Receivers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CompanyId",
                table: "Banks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CreatorId",
                table: "Banks",
                column: "CreatorId");

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
                name: "IX_Budgets_CompanyId",
                table: "Budgets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CreatorId",
                table: "Budgets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DinningRoomId",
                table: "Budgets",
                column: "DinningRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ExpenseTypeId",
                table: "Budgets",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyId",
                table: "Clients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatorId",
                table: "Clients",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DinningRoomId",
                table: "Clients",
                column: "DinningRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatorEventId",
                table: "Companies",
                column: "CreatorEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ParentId",
                table: "Companies",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Url",
                table: "Companies",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Has_Modules_CompanyId",
                table: "Company_Has_Modules",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_CompanyId",
                table: "Daily_Service_Sale",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_CreatorId",
                table: "Daily_Service_Sale",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_SaleId",
                table: "Daily_Service_Sale",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_ServiceId",
                table: "Daily_Service_Sale",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatorEventId",
                table: "Departments",
                column: "CreatorEventId");

            migrationBuilder.CreateIndex(
                name: "IX_DinningRooms_CompanyId",
                table: "DinningRooms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DinningRooms_CreatorId",
                table: "DinningRooms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CompanyId",
                table: "Expenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ProviderId",
                table: "Expenses",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_RequisitionId",
                table: "Expenses",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_CompanyId1",
                table: "ExpensesGroups",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_CreatorId",
                table: "ExpensesGroups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesGroups_TypeId",
                table: "ExpensesGroups",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_CompanyId1",
                table: "ExpenseTypes",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_CreatorId",
                table: "ExpenseTypes",
                column: "CreatorId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Lenders_CompanyId",
                table: "Lenders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lenders_CreatorId",
                table: "Lenders",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_ClientId",
                table: "Movements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_CompanyId",
                table: "Movements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_CreatorId",
                table: "Movements",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Receivers_NotificationId",
                table: "Notification_Receivers",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Receivers_UserId",
                table: "Notification_Receivers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CompanyId",
                table: "Permissions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatorEventId",
                table: "Permissions",
                column: "CreatorEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");

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
                name: "IX_Providers_CompanyId",
                table: "Providers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CreatorId",
                table: "Providers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_ApproveUserId",
                table: "Requisitions",
                column: "ApproveUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_CompanyId",
                table: "Requisitions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_ComprobateUserId",
                table: "Requisitions",
                column: "ComprobateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_CreatorId",
                table: "Requisitions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_DinningRoomId",
                table: "Requisitions",
                column: "DinningRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_LenderId",
                table: "Requisitions",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_ResponsableId",
                table: "Requisitions",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_Files_RequisitionId",
                table: "Requisitions_Files",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RootUsers_UserId",
                table: "RootUsers",
                column: "UserId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Services_CompanyId",
                table: "Services",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatorId",
                table: "Services",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_ClientId",
                table: "Services_Price_Clients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_CompanyId",
                table: "Services_Price_Clients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_CreatorId",
                table: "Services_Price_Clients",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_ServiceId",
                table: "Services_Price_Clients",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_BankId",
                table: "SmallBox",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_ClientId",
                table: "SmallBox",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_CompanyId",
                table: "SmallBox",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_CreatorId",
                table: "SmallBox",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_ProviderId",
                table: "SmallBox",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_RequisitionId",
                table: "SmallBox",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatorEventId",
                table: "Users",
                column: "CreatorEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wer_File_ReportId",
                table: "Wer_File",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_File_Company_CompanyId",
                table: "Wer_File_Company",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_File_Company_UserId",
                table: "Wer_File_Company",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_File_Company_WeekId",
                table: "Wer_File_Company",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Groups_CompanyId",
                table: "Wer_Groups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Groups_UserId",
                table: "Wer_Groups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Messages_GroupId",
                table: "Wer_Messages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Messages_UserId",
                table: "Wer_Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Messages_WeekId",
                table: "Wer_Messages",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Party_GroupId",
                table: "Wer_Party",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Party_UserId",
                table: "Wer_Party",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Reports_CompanyId",
                table: "Wer_Reports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Reports_UserId",
                table: "Wer_Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Reports_WeekId",
                table: "Wer_Reports",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Seen_Reports_ReportId",
                table: "Wer_Seen_Reports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Seen_Reports_UserId",
                table: "Wer_Seen_Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Token_User_UserId",
                table: "Wer_Token_User",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companies_CompanyId",
                table: "Invoices",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_CreatorId",
                table: "Invoices",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Banks_BankId",
                table: "Invoices",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Companies_CompanyId",
                table: "SmallBox",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Users_CreatorId",
                table: "SmallBox",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Providers_ProviderId",
                table: "SmallBox",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Requisitions_RequisitionId",
                table: "SmallBox",
                column: "RequisitionId",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Banks_BankId",
                table: "SmallBox",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallBox_Clients_ClientId",
                table: "SmallBox",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_Companies_CompanyId",
                table: "Budget_Concepts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_Users_CreatorId",
                table: "Budget_Concepts",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_Budgets_BudgetId",
                table: "Budget_Concepts",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_ExpensesGroups_ExpenseGroupId",
                table: "Budget_Concepts",
                column: "ExpenseGroupId",
                principalTable: "ExpensesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Concepts_ExpenseTypes_ExpenseTypeId",
                table: "Budget_Concepts",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Companies_CompanyId",
                table: "Movements",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Users_CreatorId",
                table: "Movements",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Clients_ClientId",
                table: "Movements",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Sales_Companies_CompanyId",
                table: "Service_Sales",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Sales_Users_CreatorId",
                table: "Service_Sales",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Sales_Clients_ClientId",
                table: "Service_Sales",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Price_Clients_Companies_CompanyId",
                table: "Services_Price_Clients",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Price_Clients_Users_CreatorId",
                table: "Services_Price_Clients",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Price_Clients_Services_ServiceId",
                table: "Services_Price_Clients",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Price_Clients_Clients_ClientId",
                table: "Services_Price_Clients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Companies_CompanyId",
                table: "Banks",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Users_CreatorId",
                table: "Banks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Companies_CompanyId",
                table: "Budgets",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_CreatorId",
                table: "Budgets",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_ExpenseTypes_ExpenseTypeId",
                table: "Budgets",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_DinningRooms_DinningRoomId",
                table: "Budgets",
                column: "DinningRoomId",
                principalTable: "DinningRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Companies_CompanyId",
                table: "Clients",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_CreatorId",
                table: "Clients",
                column: "CreatorId",
                principalTable: "Users",
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
                name: "FK_Company_Has_Modules_Companies_CompanyId",
                table: "Company_Has_Modules",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Companies_CompanyId",
                table: "Daily_Service_Sale",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Users_CreatorId",
                table: "Daily_Service_Sale",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Services_ServiceId",
                table: "Daily_Service_Sale",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Events_CreatorEventId",
                table: "Departments",
                column: "CreatorEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DinningRooms_Companies_CompanyId",
                table: "DinningRooms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DinningRooms_Users_CreatorId",
                table: "DinningRooms",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Companies_CompanyId",
                table: "Expenses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpensesGroups_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId",
                principalTable: "ExpensesGroups",
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
                name: "FK_Expenses_Requisitions_RequisitionId",
                table: "Expenses",
                column: "RequisitionId",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesGroups_Companies_CompanyId1",
                table: "ExpensesGroups",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesGroups_Users_CreatorId",
                table: "ExpensesGroups",
                column: "CreatorId",
                principalTable: "Users",
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
                name: "FK_ExpenseTypes_Companies_CompanyId1",
                table: "ExpenseTypes",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTypes_Users_CreatorId",
                table: "ExpenseTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Payments_Companies_CompanyId",
                table: "Invoice_Payments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Payments_Users_CreatorId",
                table: "Invoice_Payments",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lenders_Companies_CompanyId",
                table: "Lenders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lenders_Users_CreatorId",
                table: "Lenders",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Companies_CompanyId",
                table: "Permissions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Events_CreatorEventId",
                table: "Permissions",
                column: "CreatorEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_Companies_CompanyId",
                table: "Personal",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_Users_CreatorId",
                table: "Personal",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personal_Users_UserId",
                table: "Personal",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Companies_CompanyId",
                table: "Providers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Users_CreatorId",
                table: "Providers",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Companies_CompanyId",
                table: "Requisitions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Users_ApproveUserId",
                table: "Requisitions",
                column: "ApproveUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Users_ComprobateUserId",
                table: "Requisitions",
                column: "ComprobateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Users_CreatorId",
                table: "Requisitions",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Users_ResponsableId",
                table: "Requisitions",
                column: "ResponsableId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Companies_CompanyId",
                table: "Services",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_CreatorId",
                table: "Services",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wer_File_Company_Companies_CompanyId",
                table: "Wer_File_Company",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wer_File_Company_Users_UserId",
                table: "Wer_File_Company",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wer_Groups_Companies_CompanyId",
                table: "Wer_Groups",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wer_Groups_Users_UserId",
                table: "Wer_Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wer_Reports_Companies_CompanyId",
                table: "Wer_Reports",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wer_Reports_Users_UserId",
                table: "Wer_Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Events_CreatorEventId",
                table: "Companies",
                column: "CreatorEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_CreatorEventId",
                table: "Users",
                column: "CreatorEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Budget_Concepts");

            migrationBuilder.DropTable(
                name: "Company_Has_Modules");

            migrationBuilder.DropTable(
                name: "Daily_Service_Sale");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Invoice_Payments");

            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.DropTable(
                name: "Notification_Receivers");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Requisitions_Files");

            migrationBuilder.DropTable(
                name: "RootUsers");

            migrationBuilder.DropTable(
                name: "Services_Price_Clients");

            migrationBuilder.DropTable(
                name: "SmallBox");

            migrationBuilder.DropTable(
                name: "Wer_File");

            migrationBuilder.DropTable(
                name: "Wer_File_Company");

            migrationBuilder.DropTable(
                name: "Wer_Messages");

            migrationBuilder.DropTable(
                name: "Wer_Party");

            migrationBuilder.DropTable(
                name: "Wer_Seen_Reports");

            migrationBuilder.DropTable(
                name: "Wer_Token_User");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Service_Sales");

            migrationBuilder.DropTable(
                name: "ExpensesGroups");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Requisitions");

            migrationBuilder.DropTable(
                name: "Wer_Groups");

            migrationBuilder.DropTable(
                name: "Wer_Reports");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Lenders");

            migrationBuilder.DropTable(
                name: "Wer_Weeks");

            migrationBuilder.DropTable(
                name: "DinningRooms");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
