using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestaGames.Api.Response
{
    public class EmprestimoResponse
    {
        public int Id { get; set; }
        public string Jogo { get; set; }
        public string DeQuem { get; set; }
        public string PraQuem { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolvido { get; set; }
    }
}
