using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spending.Infrastructure.Migrations
{
    public partial class SetSomeFieldsToNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SpenderId",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Spenders",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SpenderId",
                table: "Spendings",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Spendings",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Spendings",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Spendings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Spendings",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Spenders",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
