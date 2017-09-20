using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class updateUserInfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "UserInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "UserInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "UserInfos");
        }
    }
}
