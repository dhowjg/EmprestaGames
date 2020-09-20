using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmprestaGames.Api.Migrations
{
    public partial class _20092020A : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDevolvido",
                table: "Emprestimos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDevolvido",
                table: "Emprestimos");
        }
    }
}
