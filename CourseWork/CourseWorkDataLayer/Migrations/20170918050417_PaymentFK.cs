using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class PaymentFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "ProjectNumber",
            //    table: "UserInfos");

            //migrationBuilder.AddColumn<string>(
            //    name: "About",
            //    table: "UserInfos",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Avatar",
            //    table: "UserInfos",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Contacts",
            //    table: "UserInfos",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ProjectId",
                table: "Payments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Projects_ProjectId",
                table: "Payments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Projects_ProjectId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ProjectId",
                table: "Payments");

            //migrationBuilder.DropColumn(
            //    name: "About",
            //    table: "UserInfos");

            //migrationBuilder.DropColumn(
            //    name: "Avatar",
            //    table: "UserInfos");

            //migrationBuilder.DropColumn(
            //    name: "Contacts",
            //    table: "UserInfos");

            //migrationBuilder.AddColumn<int>(
            //    name: "ProjectNumber",
            //    table: "UserInfos",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
