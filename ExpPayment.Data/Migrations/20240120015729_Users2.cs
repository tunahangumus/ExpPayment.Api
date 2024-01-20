using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpPayment.Data.Migrations
{
    /// <inheritdoc />
    public partial class Users2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Invoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Expenses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Expenses");

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "ApplicationUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "ApplicationUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "ApplicationUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "ApplicationUsers",
                type: "integer",
                nullable: true);
        }
    }
}
