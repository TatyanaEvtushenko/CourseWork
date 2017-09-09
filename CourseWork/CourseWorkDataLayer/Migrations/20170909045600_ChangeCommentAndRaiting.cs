using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class ChangeCommentAndRaiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Raitings",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Raitings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Comments",
                newName: "UserId");
        }
    }
}
