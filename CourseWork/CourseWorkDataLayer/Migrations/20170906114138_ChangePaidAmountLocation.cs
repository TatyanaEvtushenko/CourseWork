using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class ChangePaidAmountLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "FinancialPurposes");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Projects",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatingTime",
                table: "FinancialPurposes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsReached",
                table: "FinancialPurposes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PassportScan = table.Column<string>(nullable: true),
                    ProjectNumber = table.Column<int>(nullable: false),
                    Raiting = table.Column<int>(nullable: false),
                    RegistrationTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatingTime",
                table: "FinancialPurposes");

            migrationBuilder.DropColumn(
                name: "IsReached",
                table: "FinancialPurposes");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "FinancialPurposes",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
