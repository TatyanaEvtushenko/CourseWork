using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class TotalDBRestructuring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Projects_ProjectId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserInfos",
                nullable: false,
                maxLength: 256,
                oldClrType: typeof(string));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments"
            );

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                nullable: true,
                maxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserName",
            //    table: "Ratings",
            //    nullable: true,
            //    maxLength: 256,
            //    oldClrType: typeof(string));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Ratings"
            );

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Ratings",
                nullable: true,
                maxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Ratings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserName",
                table: "Projects",
                nullable: true,
                maxLength: 256,
                oldClrType: typeof(string));

            //migrationBuilder.DropColumn(
            //    name: "OwnerUserName",
            //    table: "Projects"
            //);

            //migrationBuilder.AddColumn<string>(
            //    name: "OwnerUserName",
            //    table: "Projects",
            //    nullable: true,
            //    maxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Projects",
                nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserName",
            //    table: "Payments",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Payments"
            );

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Payments",
                nullable: true,
                maxLength: 256);

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
                name: "UserName",
                table: "ProjectSubscribers",
                nullable: false,
                maxLength: 256,
                oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "RecipientUserName",
            //    table: "Messages",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            migrationBuilder.DropColumn(
                name: "RecipientUserName",
                table: "Messages"
            );

            migrationBuilder.AddColumn<string>(
                name: "RecipientUserName",
                table: "Messages",
                nullable: true,
                maxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "FinancialPurposes",
                nullable: true,
                oldClrType: typeof(string));

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserName",
            //    table: "Comments",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProjectId",
                table: "Ratings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserName",
                table: "Ratings",
                column: "UserName");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProjectSubscribers_ProjectId",
            //    table: "ProjectSubscribers",
            //    column: "ProjectId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSubscribers",
                table: "ProjectSubscribers",
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
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects",
                column: "OwnerUserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_AspNetUsers_UserName",
                table: "UserInfos",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubscribers_UserInfos_UserName",
                table: "ProjectSubscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Projects_ProjectId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_UserInfos_UserName",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_AspNetUsers_UserName",
                table: "UserInfos");

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

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserInfos",
                nullable: false,
                oldClrType: typeof(string));

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

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserInfos_OwnerUserName",
                table: "Projects",
                column: "OwnerUserName",
                principalTable: "UserInfos",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
