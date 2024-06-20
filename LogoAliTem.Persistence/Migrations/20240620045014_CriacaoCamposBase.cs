using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class CriacaoCamposBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Veiculos",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Veiculos",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Motoristas",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Motoristas",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Motoristas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Motoristas");
        }
    }
}
