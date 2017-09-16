using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class AddForeignKeyToSubscriber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubscribers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubscribers_ProjectId",
                table: "ProjectSubscribers",
                column: "ProjectId");

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

            migrationBuilder.DropIndex(
                name: "IX_ProjectSubscribers_ProjectId",
                table: "ProjectSubscribers");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubscribers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
