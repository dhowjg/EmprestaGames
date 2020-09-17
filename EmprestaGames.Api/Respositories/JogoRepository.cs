using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public class JogoRepository : IJogoRepository
    {
        public List<Jogo> Get()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.Jogos.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Jogo GetId(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    return db.Jogos.Find(Id);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Jogo Inserir(Jogo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var jogo = model;
                    db.Jogos.Add(model);                    
                    db.SaveChanges();

                    return jogo;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool Remove(Jogo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    
                    db.Jogos.Remove(model);                    
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