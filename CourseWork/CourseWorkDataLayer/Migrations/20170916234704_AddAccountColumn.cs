using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class AddAccountColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "LastAccountNumber",
                table: "UserInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAccountNumber",
                table: "UserInfos");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Projects",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
