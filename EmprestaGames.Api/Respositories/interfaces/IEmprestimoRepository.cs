using System.Collections.Generic;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Response;

namespace EmprestaGames.Api.Respositories
{
    public interface IEmprestimoRepository
    {
        Emprestimo Inserir(Emprestimo model);
        bool Remove(int Id);
        bool Devolver(int Id);
        Emprestimo GetId(int Id);
        List<EmprestimoResponse> Get();
        List<EmprestimoResponse> GetSoEmprestado();

    }
}