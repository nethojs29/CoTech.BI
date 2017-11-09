using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class movementsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RequisitionId",
                table: "SmallBox",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Movements",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SmallBox_RequisitionId",
                table: "SmallBox",
                column: "RequisitionId");

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
                name: "FK_SmallBox_Requisitions_RequisitionId",
                table: "SmallBox");

            migrationBuilder.DropIndex(
                name: "IX_SmallBox_RequisitionId",
                table: "SmallBox");

            migrationBuilder.DropColumn(
                name: "RequisitionId",
                table: "SmallBox");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Movements");
        }
    }
}
