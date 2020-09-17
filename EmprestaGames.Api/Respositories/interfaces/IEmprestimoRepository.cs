using System.Collections.Generic;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public interface IEmprestimoRepository
    {
        Emprestimo Inserir(Emprestimo model);
        bool Remove(Emprestimo model);
        Emprestimo GetId(int Id);
        List<Emprestimo> Get();
    }
}