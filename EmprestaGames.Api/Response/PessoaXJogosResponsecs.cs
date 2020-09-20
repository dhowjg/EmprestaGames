using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestaGames.Api.Response
{
    public class PessoaXJogosResponsecs
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public int PessoaId { get; set; }
        public string NomeJogo { get; set; }
        public string NomePessoa { get; set; }
    }
}
