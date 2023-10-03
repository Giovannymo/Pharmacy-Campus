using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration01102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_JobTitle_JobTitle_Fk",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "JobTitle_Fk",
                table: "Person",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Person",
                type: "DATETIME",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_JobTitle_JobTitle_Fk",
                table: "Person",
                column: "JobTitle_Fk",
                principalTable: "JobTitle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_JobTitle_JobTitle_Fk",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "JobTitle_Fk",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Person",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_JobTitle_JobTitle_Fk",
                table: "Person",
                column: "JobTitle_Fk",
                principalTable: "JobTitle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
