using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoTech.Bi.Migrations
{
    public partial class Res : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Wer_File",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Wer_File",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                    table.ForeignKey(
                        name: "FK_Wer_Groups_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_Groups_Users_UserId",
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
                name: "PartyEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyEntity_Wer_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Wer_Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyEntity_Users_UserId",
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
                    UserId = table.Column<long>(type: "bigint", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Wer_Seen_Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    SeenAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wer_Seen_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wer_Seen_Messages_Wer_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Wer_Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wer_Seen_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyEntity_GroupId",
                table: "PartyEntity",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyEntity_UserId",
                table: "PartyEntity",
                column: "UserId");

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
                name: "IX_Wer_Seen_Messages_MessageId",
                table: "Wer_Seen_Messages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Seen_Messages_UserId",
                table: "Wer_Seen_Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Seen_Reports_ReportId",
                table: "Wer_Seen_Reports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Wer_Seen_Reports_UserId",
                table: "Wer_Seen_Reports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyEntity");

            migrationBuilder.DropTable(
                name: "Wer_Seen_Messages");

            migrationBuilder.DropTable(
                name: "Wer_Seen_Reports");

            migrationBuilder.DropTable(
                name: "Wer_Messages");

            migrationBuilder.DropTable(
                name: "Wer_Groups");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Wer_File");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Wer_File");
        }
    }
}
