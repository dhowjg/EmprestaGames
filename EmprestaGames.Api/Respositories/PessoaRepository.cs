using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public class PessoaRespository : IPessoaRespository
    {
        public List<Pessoa> Get()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.Pessoas.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Pessoa GetId(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.Pessoas.Find(Id);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Pessoa Inserir(Pessoa model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var pessoa = model;
                    db.Pessoas.Add(model);                    
                    db.SaveChanges();



                    return pessoa;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool Remove(Pessoa model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    
                    db.Pessoas.Remove(model);                    
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