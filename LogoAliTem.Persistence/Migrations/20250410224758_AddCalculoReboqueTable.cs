using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class AddCalculoReboqueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculosReboque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoVeiculo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LocalRetirada = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LocalDestino = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DistanciaTotal = table.Column<double>(type: "double precision", nullable: false),
                    ValorCalculado = table.Column<double>(type: "double precision", nullable: false),
                    DataCalculo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculosReboque", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculosReboque");
        }
    }
}
