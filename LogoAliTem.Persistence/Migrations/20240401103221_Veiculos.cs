using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class Veiculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Veiculos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimentoCNH",
                table: "Motoristas",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Motoristas",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_UserId",
                table: "Veiculos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_AspNetUsers_UserId",
                table: "Veiculos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_AspNetUsers_UserId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_UserId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Veiculos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimentoCNH",
                table: "Motoristas",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Motoristas",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
