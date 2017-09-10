using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class ChangeNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Raitings",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectSubscribers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Payments",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "Messages",
                newName: "RecipientUserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "UserName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "News",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "News",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Raitings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "ProjectSubscribers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Payments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RecipientUserName",
                table: "Messages",
                newName: "RecipientId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "News",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
