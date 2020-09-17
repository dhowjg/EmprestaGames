using System.Collections.Generic;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public interface IPessoaXJogoRepository
    {
        PessoaXJogo Inserir(PessoaXJogo model);
        bool Remove(PessoaXJogo model);
        PessoaXJogo GetId(int Id);
        List<PessoaXJogo> Get();
    }
}