using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class ArquivoEstadia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Arquivo",
                table: "CertidaoEstadia",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arquivo",
                table: "CertidaoEstadia");
        }
    }
}
