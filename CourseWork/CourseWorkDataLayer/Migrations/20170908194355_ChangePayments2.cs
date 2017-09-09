using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class ChangePayments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Projects");

            //migrationBuilder.CreateTable(
            //    name: "Projects",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        CreatingTime = table.Column<DateTime>(nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        FundRaisingEnd = table.Column<DateTime>(nullable: false),
            //        ImageUrl = table.Column<string>(nullable: true),
            //        MaxPayment = table.Column<decimal>(nullable: false),
            //        MinPayment = table.Column<decimal>(nullable: false),
            //        Name = table.Column<string>(nullable: true),
            //        OwnerUserName = table.Column<string>(nullable: true),
            //        Status = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Projects", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Projects");
        }
    }
}
