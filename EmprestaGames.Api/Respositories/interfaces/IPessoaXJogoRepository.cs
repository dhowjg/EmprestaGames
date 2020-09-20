using System.Collections.Generic;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Response;

namespace EmprestaGames.Api.Respositories
{
    public interface IPessoaXJogoRepository
    {
        PessoaXJogo Inserir(PessoaXJogo model);
        bool Remove(int Id);
        PessoaXJogo GetId(int Id);
        List<PessoaXJogosResponsecs> Get();
        List<Pessoa> GetPessoa(int JogoId);
        List<Jogo> GetJogosNaoEmprestados();
    }
}