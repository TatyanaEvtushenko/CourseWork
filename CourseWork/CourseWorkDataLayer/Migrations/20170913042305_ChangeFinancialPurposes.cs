using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.DataLayer.Migrations
{
    public partial class ChangeFinancialPurposes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "FinancialPurposes",
                nullable: false);

            migrationBuilder.AddColumn<decimal>(
                name: "NecessaryPaymentAmount",
                table: "FinancialPurposes",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "FinancialPurposes");

            migrationBuilder.DropColumn(
                name: "NecessaryPaymentAmount",
                table: "FinancialPurposes");
        }
    }
}
