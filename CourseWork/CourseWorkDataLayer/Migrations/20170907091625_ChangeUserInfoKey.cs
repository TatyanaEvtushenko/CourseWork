using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class ChangeUserInfoKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "UserId",
            //    table: "UserInfos",
            //    newName: "UserName");

            //migrationBuilder.RenameColumn(
            //    name: "OwnerId",
            //    table: "Projects",
            //    newName: "OwnerUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "UserName",
            //    table: "UserInfos",
            //    newName: "UserId");

            //migrationBuilder.RenameColumn(
            //    name: "OwnerUserName",
            //    table: "Projects",
            //    newName: "OwnerId");
        }
    }
}
