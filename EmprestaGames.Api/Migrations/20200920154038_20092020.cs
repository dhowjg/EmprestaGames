using Microsoft.EntityFrameworkCore.Migrations;

namespace EmprestaGames.Api.Migrations
{
    public partial class _20092020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaEmprestadaId",
                table: "Emprestimos");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaEmprestadaId",
                table: "Emprestimos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PessoaDonoJogoId",
                table: "Emprestimos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_PessoaDonoJogoId",
                table: "Emprestimos",
                column: "PessoaDonoJogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaDonoJogoId",
                table: "Emprestimos",
                column: "PessoaDonoJogoId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaEmprestadaId",
                table: "Emprestimos",
                column: "PessoaEmprestadaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaDonoJogoId",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaEmprestadaId",
                table: "Emprestimos");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_PessoaDonoJogoId",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "PessoaDonoJogoId",
                table: "Emprestimos");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaEmprestadaId",
                table: "Emprestimos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Pessoas_PessoaEmprestadaId",
                table: "Emprestimos",
                column: "PessoaEmprestadaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
