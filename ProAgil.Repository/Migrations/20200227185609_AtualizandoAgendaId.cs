using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class AtualizandoAgendaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgendaId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgendaId",
                table: "AspNetUsers");
        }
    }
}
