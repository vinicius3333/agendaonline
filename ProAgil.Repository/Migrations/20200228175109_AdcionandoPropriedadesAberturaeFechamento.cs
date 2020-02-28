using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class AdcionandoPropriedadesAberturaeFechamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Abertura",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Fechamento",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abertura",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fechamento",
                table: "AspNetUsers");
        }
    }
}
