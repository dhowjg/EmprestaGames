using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestaGames.Api.Models
{
    [Table("Emprestimos")]
    public class Emprestimo
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public int PessoaEmprestadaId { get; set; }
        public int JogoId { get; set; }

        public virtual Pessoa PessoaEmprestada { get; set; }
        public virtual Jogo Jogo { get; set; }

    }

}