using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class TotalTotalRestructuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Payments_Projects_ProjectId",
            //    table: "Payments");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Ratings_ProjectId_UserName",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectSubscribers_ProjectId_UserName",
                table: "ProjectSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Ratings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserName",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Projects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "News",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RecipientUserName",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "FinancialPurposes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProjectId",
                table: "Ratings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserName",
                table: "Ratings",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubscribers_ProjectId",
                table: "ProjectSubscribers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserName",
                table: "Payments",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientUserName",
                table: "Messages",
                column: "RecipientUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserName",
                table: "Comments",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserInfos_UserName",
                table: "Comments",
                column: "UserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserInfos_RecipientUserName",
                table: "Messages",
                column: "RecipientUserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Projects_ProjectId",
                table: "Payments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_UserInfos_UserName",
                table: "Payments",
                column: "UserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubscribers_UserInfos_UserName",
                table: "ProjectSubscribers",
                column: "UserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Projects_ProjectId",
                table: "Ratings",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_UserInfos_UserName",
                table: "Ratings",
                column: "UserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserInfos_UserName",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserInfos_RecipientUserName",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Projects_ProjectId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_UserInfos_UserName",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubscribers_UserInfos_UserName",
                table: "ProjectSubscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Projects_ProjectId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_UserInfos_UserName",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ProjectId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserName",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_ProjectSubscribers_ProjectId",
                table: "ProjectSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserName",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipientUserName",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserName",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Projects");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "UserInfos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserName",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Projects",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "News",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecipientUserName",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "FinancialPurposes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Ratings_ProjectId_UserName",
                table: "Ratings",
                columns: new[] { "ProjectId", "UserName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                columns: new[] { "UserName", "ProjectId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectSubscribers_ProjectId_UserName",
                table: "ProjectSubscribers",
                columns: new[] { "ProjectId", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Projects_ProjectId",
                table: "Payments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
