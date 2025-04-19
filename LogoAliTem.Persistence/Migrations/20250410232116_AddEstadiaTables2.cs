using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class AddEstadiaTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertidoesEstadia_CalculosEstadia_CalculoEstadiaId",
                table: "CertidoesEstadia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CertidoesEstadia",
                table: "CertidoesEstadia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculosEstadia",
                table: "CalculosEstadia");

            migrationBuilder.RenameTable(
                name: "CertidoesEstadia",
                newName: "CertidaoEstadia");

            migrationBuilder.RenameTable(
                name: "CalculosEstadia",
                newName: "CalculoEstadia");

            migrationBuilder.RenameIndex(
                name: "IX_CertidoesEstadia_CalculoEstadiaId",
                table: "CertidaoEstadia",
                newName: "IX_CertidaoEstadia_CalculoEstadiaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CertidaoEstadia",
                table: "CertidaoEstadia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculoEstadia",
                table: "CalculoEstadia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CertidaoEstadia_CalculoEstadia_CalculoEstadiaId",
                table: "CertidaoEstadia",
                column: "CalculoEstadiaId",
                principalTable: "CalculoEstadia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertidaoEstadia_CalculoEstadia_CalculoEstadiaId",
                table: "CertidaoEstadia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CertidaoEstadia",
                table: "CertidaoEstadia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculoEstadia",
                table: "CalculoEstadia");

            migrationBuilder.RenameTable(
                name: "CertidaoEstadia",
                newName: "CertidoesEstadia");

            migrationBuilder.RenameTable(
                name: "CalculoEstadia",
                newName: "CalculosEstadia");

            migrationBuilder.RenameIndex(
                name: "IX_CertidaoEstadia_CalculoEstadiaId",
                table: "CertidoesEstadia",
                newName: "IX_CertidoesEstadia_CalculoEstadiaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CertidoesEstadia",
                table: "CertidoesEstadia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculosEstadia",
                table: "CalculosEstadia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CertidoesEstadia_CalculosEstadia_CalculoEstadiaId",
                table: "CertidoesEstadia",
                column: "CalculoEstadiaId",
                principalTable: "CalculosEstadia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
