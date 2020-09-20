using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;
using Microsoft.EntityFrameworkCore;

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
                    if (model.Id > 0)
                    {
                        var exists = db.Jogos.Find(model.Id);
                        if (exists == null) return null;
                        exists.Descricao = model.Descricao;
                        exists.Nome = model.Nome;
                        db.Entry(exists).State = EntityState.Modified;

                    }
                    else
                    {
                        db.Jogos.Add(model);
                    }
                    
                                       
                    db.SaveChanges();

                    return jogo;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool Remove(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    if (Id == 0) return false;

                    var model = db.Jogos.Find(Id);
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