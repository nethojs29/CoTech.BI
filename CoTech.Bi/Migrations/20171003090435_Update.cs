using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Requisitions");

            migrationBuilder.AlterColumn<float>(
                name: "Refund",
                table: "Requisitions",
                type: "float",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Requisitions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ComprobateDate",
                table: "Requisitions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "Requisitions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "Requisitions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LenderId",
                table: "Requisitions",
                type: "bigint",
                nullable: true);

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
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Departments_Events_CreatorEventId",
                        column: x => x.CreatorEventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_Lenders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movements_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
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
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmallBox",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Concept = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProviderId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmallBox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmallBox_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmallBox_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmallBox_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Daily_Service_Sale",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    iva = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daily_Service_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Daily_Service_Sale_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Daily_Service_Sale_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Daily_Service_Sale_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Price = table.Column<float>(type: "float", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services_Price_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Price_Clients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Price_Clients_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Price_Clients_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_BankId",
                table: "Requisitions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_LenderId",
                table: "Requisitions",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_ClientId",
                table: "Daily_Service_Sale",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Service_Sale_CompanyId",
                table: "Daily_Service_Sale",
                column: "CompanyId");

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
                name: "IX_Lenders_CompanyId",
                table: "Lenders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_ClientId",
                table: "Movements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Movements_CompanyId",
                table: "Movements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CompanyId",
                table: "Services",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_ClientId",
                table: "Services_Price_Clients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_CompanyId",
                table: "Services_Price_Clients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price_Clients_ServiceId",
                table: "Services_Price_Clients",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_ClientId",
                table: "SmallBox",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_CompanyId",
                table: "SmallBox",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_ProviderId",
                table: "SmallBox",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Banks_BankId",
                table: "Requisitions",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_Lenders_LenderId",
                table: "Requisitions",
                column: "LenderId",
                principalTable: "Lenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requisitions_Banks_BankId",
                table: "Requisitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisitions_Lenders_LenderId",
                table: "Requisitions");

            migrationBuilder.DropTable(
                name: "Daily_Service_Sale");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Lenders");

            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.DropTable(
                name: "Services_Price_Clients");

            migrationBuilder.DropTable(
                name: "SmallBox");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Requisitions_BankId",
                table: "Requisitions");

            migrationBuilder.DropIndex(
                name: "IX_Requisitions_LenderId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "LenderId",
                table: "Requisitions");

            migrationBuilder.AlterColumn<float>(
                name: "Refund",
                table: "Requisitions",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Requisitions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ComprobateDate",
                table: "Requisitions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "Requisitions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "Requisitions",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
