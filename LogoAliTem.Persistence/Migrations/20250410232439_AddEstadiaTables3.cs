using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoAliTem.Persistence.Migrations
{
    public partial class AddEstadiaTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculosReboque",
                table: "CalculosReboque");

            migrationBuilder.RenameTable(
                name: "CalculosReboque",
                newName: "CalculoReboque");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculoReboque",
                table: "CalculoReboque",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculoReboque",
                table: "CalculoReboque");

            migrationBuilder.RenameTable(
                name: "CalculoReboque",
                newName: "CalculosReboque");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculosReboque",
                table: "CalculosReboque",
                column: "Id");
        }
    }
}
