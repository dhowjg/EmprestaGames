using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestaGames.Api.Models
{
    [Table("PessoasXJogos")]
    public class PessoaXJogo
    {
        [Key]
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int JogoId { get; set; }

        public virtual Pessoa Pessoas { get; set; }
        public virtual Jogo Jogos { get; set; }

    }

}