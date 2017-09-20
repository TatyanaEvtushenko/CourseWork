using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class TotalTotalRestructuring4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
