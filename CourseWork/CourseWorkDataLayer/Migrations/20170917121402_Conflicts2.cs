using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class Conflicts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "OwnerUserName",
            //    table: "Projects",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProjectId",
            //    table: "News",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProjectId",
            //    table: "FinancialPurposes",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProjectId",
            //    table: "Comments",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tags_ProjectId",
            //    table: "Tags",
            //    column: "ProjectId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Projects_OwnerUserName",
            //    table: "Projects",
            //    column: "OwnerUserName");

            //migrationBuilder.CreateIndex(
            //    name: "IX_News_ProjectId",
            //    table: "News",
            //    column: "ProjectId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FinancialPurposes_ProjectId",
            //    table: "FinancialPurposes",
            //    column: "ProjectId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_ProjectId",
            //    table: "Comments",
            //    column: "ProjectId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comments_Projects_ProjectId",
            //    table: "Comments",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FinancialPurposes_Projects_ProjectId",
            //    table: "FinancialPurposes",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_News_Projects_ProjectId",
            //    table: "News",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Projects_UserInfos_OwnerUserName",
            //    table: "Projects",
            //    column: "OwnerUserName",
            //    principalTable: "UserInfos",
            //    principalColumn: "UserName",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectSubscribers_Projects_ProjectId",
            //    table: "ProjectSubscribers",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Tags_Projects_ProjectId",
            //    table: "Tags",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Comments_Projects_ProjectId",
            //    table: "Comments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_FinancialPurposes_Projects_ProjectId",
            //    table: "FinancialPurposes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_News_Projects_ProjectId",
            //    table: "News");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Projects_UserInfos_OwnerUserName",
            //    table: "Projects");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectSubscribers_Projects_ProjectId",
            //    table: "ProjectSubscribers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tags_Projects_ProjectId",
            //    table: "Tags");

            //migrationBuilder.DropIndex(
            //    name: "IX_Tags_ProjectId",
            //    table: "Tags");

            //migrationBuilder.DropIndex(
            //    name: "IX_Projects_OwnerUserName",
            //    table: "Projects");

            //migrationBuilder.DropIndex(
            //    name: "IX_News_ProjectId",
            //    table: "News");

            //migrationBuilder.DropIndex(
            //    name: "IX_FinancialPurposes_ProjectId",
            //    table: "FinancialPurposes");

            //migrationBuilder.DropIndex(
            //    name: "IX_Comments_ProjectId",
            //    table: "Comments");

            //migrationBuilder.AlterColumn<string>(
            //    name: "OwnerUserName",
            //    table: "Projects",
            //    nullable: true,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProjectId",
            //    table: "News",
            //    nullable: true,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProjectId",
            //    table: "FinancialPurposes",
            //    nullable: true,
            //    oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "ProjectId",
            //    table: "Comments",
            //    nullable: true,
            //    oldClrType: typeof(string));
        }
    }
}
