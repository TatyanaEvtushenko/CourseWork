using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class TotalProjectRestruscturing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raitings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectSubscribers");

            migrationBuilder.DropColumn(
                name: "IsReached",
                table: "FinancialPurposes");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ProjectSubscribers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubscribers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                columns: new[] { "Name", "ProjectId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectSubscribers_ProjectId_UserName",
                table: "ProjectSubscribers",
                columns: new[] { "ProjectId", "UserName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers",
                columns: new[] { "UserName", "ProjectId" });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: false),
                    RaitingResult = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserName, x.ProjectId });
                    table.UniqueConstraint("AK_Ratings_ProjectId_UserName", x => new { x.ProjectId, x.UserName });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectSubscribers_ProjectId_UserName",
                table: "ProjectSubscribers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Tags",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubscribers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "ProjectSubscribers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ProjectSubscribers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsReached",
                table: "FinancialPurposes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Raitings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    RaitingResult = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raitings", x => x.Id);
                });
        }
    }
}
