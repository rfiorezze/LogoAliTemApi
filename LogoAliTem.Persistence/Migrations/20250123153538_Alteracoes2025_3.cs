using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class Alteracoes2025_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "AspNetUsers",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sexo",
                table: "AspNetUsers",
                type: "integer",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1);
        }
    }
}
