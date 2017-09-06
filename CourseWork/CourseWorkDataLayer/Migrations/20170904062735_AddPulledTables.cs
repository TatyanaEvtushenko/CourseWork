using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class AddPulledTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPurposes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PaymantAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPurposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPurposeInProjects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FinancialPurposeId = table.Column<string>(nullable: true),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPurposeInProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    RecipientId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FinancialPurposeId = table.Column<string>(nullable: true),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FundRaisingEnd = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    MaxPayment = table.Column<decimal>(nullable: false),
                    MinPayment = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubscribers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raitings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    RaitingResult = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raitings", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Tags",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        Name = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tags", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TagInProjects",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        ProjectId = table.Column<string>(nullable: true),
            //        TagId = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TagInProjects", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserInfos",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        IsBlocked = table.Column<bool>(nullable: false),
            //        LastLoginTime = table.Column<DateTime>(nullable: false),
            //        Name = table.Column<string>(nullable: true),
            //        PassportScan = table.Column<string>(nullable: true),
            //        ProjectNumber = table.Column<int>(nullable: false),
            //        Raiting = table.Column<int>(nullable: false),
            //        RegistrationTime = table.Column<DateTime>(nullable: false),
            //        Status = table.Column<int>(nullable: false),
            //        Surname = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserInfos", x => x.UserId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FinancialPurposes");

            migrationBuilder.DropTable(
                name: "FinancialPurposeInProjects");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectSubscribers");

            migrationBuilder.DropTable(
                name: "Raitings");

            //migrationBuilder.DropTable(
            //    name: "Tags");

            //migrationBuilder.DropTable(
            //    name: "TagInProjects");

            //migrationBuilder.DropTable(
            //    name: "UserInfos");
        }
    }
}
