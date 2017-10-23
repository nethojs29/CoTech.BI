using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class ssaleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Daily_Service_Sale");

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Service_Sales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "Daily_Service_Sale",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Service_Sales_ClientId",
                table: "Service_Sales",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Sales_Clients_ClientId",
                table: "Service_Sales",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Sales_Clients_ClientId",
                table: "Service_Sales");

            migrationBuilder.DropIndex(
                name: "IX_Service_Sales_ClientId",
                table: "Service_Sales");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Service_Sales");

            migrationBuilder.AlterColumn<long>(
                name: "ClientId",
                table: "Daily_Service_Sale",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Daily_Service_Sale",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Daily_Service_Sale_Clients_ClientId",
                table: "Daily_Service_Sale",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
