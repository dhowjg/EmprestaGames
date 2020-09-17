using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        public List<Emprestimo> Get()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.Emprestimos.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Emprestimo GetId(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.Emprestimos.Find(Id);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Emprestimo Inserir(Emprestimo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var pessoa = model;
                    db.Emprestimos.Add(model);                    
                    db.SaveChanges();



                    return pessoa;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool Remove(Emprestimo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    
                    db.Emprestimos.Remove(model);                    
                    db.SaveChanges();

                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}