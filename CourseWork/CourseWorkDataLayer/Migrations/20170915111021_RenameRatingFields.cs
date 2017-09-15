using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class RenameRatingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Raiting",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "RaitingResult",
                table: "Ratings",
                newName: "RatingResult");

            migrationBuilder.RenameColumn(
                name: "Raiting",
                table: "Projects",
                newName: "Rating");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "UserInfos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "RatingResult",
                table: "Ratings",
                newName: "RaitingResult");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Projects",
                newName: "Raiting");

            migrationBuilder.AddColumn<int>(
                name: "Raiting",
                table: "UserInfos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
