using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class TotalTotalRestructuring3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Projects_UserInfos_OwnerUserName",
            //    table: "Projects");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectSubscribers_Projects_ProjectId",
            //    table: "ProjectSubscribers");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects",
                column: "OwnerUserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects",
                column: "OwnerUserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubscribers_Projects_ProjectId",
                table: "ProjectSubscribers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
