using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpPayment.Data.Migrations
{
    /// <inheritdoc />
    public partial class Users12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Expenses_ExpenseId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ExpenseId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Invoices");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PaymentDemands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PaymentDemands",
                type: "character varying(90)",
                maxLength: 90,
                nullable: false,
                defaultValue: " ");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BillingAddress",
                table: "Invoices",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PaymentDemands");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PaymentDemands");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "BillingAddress",
                table: "Invoices",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ExpenseId",
                table: "Invoices",
                column: "ExpenseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Expenses_ExpenseId",
                table: "Invoices",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
