using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class TotalTotalRestructuring5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "UserName",
            //    table: "UserInfos",
            //    nullable: false,
            //    oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
             //   maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
            //    oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_AspNetUsers_UserName",
                table: "UserInfos",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_AspNetUsers_UserName",
                table: "UserInfos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserInfos",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
