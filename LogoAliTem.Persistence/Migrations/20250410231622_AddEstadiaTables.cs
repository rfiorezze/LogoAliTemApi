using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class AddEstadiaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculosEstadia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataChegada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraChegada = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraSaida = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CapacidadeCargaVeiculo = table.Column<double>(type: "double precision", nullable: false),
                    ValorCalculado = table.Column<double>(type: "double precision", nullable: false),
                    DataCalculo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculosEstadia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertidoesEstadia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CalculoEstadiaId = table.Column<int>(type: "integer", nullable: false),
                    Placa = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RNTRC = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NomeMotorista = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CpfCnpjMotorista = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EmailMotorista = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TelefoneMotorista = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CepMotorista = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    LogradouroMotorista = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroMotorista = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ComplementoMotorista = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BairroMotorista = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CidadeMotorista = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EstadoMotorista = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CpfCnpjLocalCarga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    NomeLocalCarga = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    EmailLocalCarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TelefoneLocalCarga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CepLocalCarga = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    LogradouroLocalCarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroLocalCarga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ComplementoLocalCarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BairroLocalCarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CidadeLocalCarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EstadoLocalCarga = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CpfCnpjLocalDescarga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    NomeLocalDescarga = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    EmailLocalDescarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TelefoneLocalDescarga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CepLocalDescarga = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    LogradouroLocalDescarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroLocalDescarga = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ComplementoLocalDescarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BairroLocalDescarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CidadeLocalDescarga = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EstadoLocalDescarga = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CteCiotContratante = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CpfCnpjContratante = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NomeContratante = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    EmailContratante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TelefoneContratante = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CepContratante = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    LogradouroContratante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NumeroContratante = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ComplementoContratante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BairroContratante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CidadeContratante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EstadoContratante = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertidoesEstadia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertidoesEstadia_CalculosEstadia_CalculoEstadiaId",
                        column: x => x.CalculoEstadiaId,
                        principalTable: "CalculosEstadia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertidoesEstadia_CalculoEstadiaId",
                table: "CertidoesEstadia",
                column: "CalculoEstadiaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertidoesEstadia");

            migrationBuilder.DropTable(
                name: "CalculosEstadia");
        }
    }
}
