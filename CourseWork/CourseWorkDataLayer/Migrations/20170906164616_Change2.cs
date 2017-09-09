using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class Change2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "UserInfos",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        IsBlocked = table.Column<bool>(nullable: false),
            //        LastLoginTime = table.Column<DateTime>(nullable: false),
            //        Name = table.Column<string>(nullable: true),
            //        PassportScan = table.Column<string>(nullable: true),
            //        ProjectNumber = table.Column<int>(nullable: false),
            //        Raiting = table.Column<int>(nullable: false),
            //        RegistrationTime = table.Column<DateTime>(nullable: false),
            //        Status = table.Column<int>(nullable: false),
            //        Surname = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserInfos", x => x.UserId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "UserInfos");
        }
    }
}
