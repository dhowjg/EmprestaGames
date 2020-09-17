using System.Collections.Generic;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public interface IJogoRepository
    {
        Jogo Inserir(Jogo model);
        bool Remove(Jogo model);
        Jogo GetId(int Id);
        List<Jogo> Get();
    }
}