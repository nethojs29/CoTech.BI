using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "FK_Wer_File_Company_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_File_Company_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_File_Company_Wer_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Wer_Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wer_File_Company");
        }
    }
}
