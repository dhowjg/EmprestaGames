using System.Collections.Generic;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public interface IPessoaRespository
    {
        Pessoa Inserir(Pessoa model);
        bool Remove(Pessoa model);
        Pessoa GetId(int Id);
        List<Pessoa> Get();
    }
}