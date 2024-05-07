using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class CriandoChassirENAVAM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chassi",
                table: "Veiculos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Renavam",
                table: "Veiculos",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chassi",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Renavam",
                table: "Veiculos");
        }
    }
}
