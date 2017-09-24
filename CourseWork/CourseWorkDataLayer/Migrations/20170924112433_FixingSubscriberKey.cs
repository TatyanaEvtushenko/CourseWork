using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class FixingSubscriberKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubscribers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers",
                columns: new[] { "UserName", "ProjectId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubscribers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers",
                column: "UserName");
        }
    }
}
