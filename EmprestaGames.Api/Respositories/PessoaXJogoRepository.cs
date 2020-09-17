using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public class PessoaXJogoRepository : IPessoaXJogoRepository
    {
        public List<PessoaXJogo> Get()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.PessoasXJogos.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PessoaXJogo GetId(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.PessoasXJogos.Find(Id);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PessoaXJogo Inserir(PessoaXJogo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var jogo = model;
                    db.PessoasXJogos.Add(model);                    
                    db.SaveChanges();

                    return jogo;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool Remove(PessoaXJogo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    
                    db.PessoasXJogos.Remove(model);                    
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