using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class CreateNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Payments",
                newName: "UserName");

            migrationBuilder.CreateTable(
                    name: "News",
                    columns: table => new
                    {
                        Id = table.Column<string>(nullable: false),
                        Time = table.Column<DateTime>(nullable: false),
                        ProjectId = table.Column<string>(nullable: false),
                        Subject = table.Column<string>(nullable: true),
                        Text = table.Column<string>(nullable: false),
                        Type = table.Column<int>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_News", x => x.Id);
                    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                    name: "News");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Payments",
                newName: "UserId");
        }
    }
}
