using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using CoTech.Bi.Loader;

namespace CoTech.Bi.Migrations
{
    public partial class Seedv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.BiSeedUp(1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.BiSeedDown(1);
        }
    }
}
