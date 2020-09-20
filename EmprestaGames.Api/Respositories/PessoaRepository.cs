using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;
using Microsoft.EntityFrameworkCore;

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
                    if (model.Id > 0)
                    {
                        var exists = db.Pessoas.Find(model.Id);
                        if (exists == null) return null;                        
                        exists.Nome = model.Nome;
                        db.Entry(exists).State = EntityState.Modified;

                    }
                    else
                    {
                        db.Pessoas.Add(pessoa);
                    }

                    db.SaveChanges();
                    return pessoa;
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

                    var model = db.Pessoas.Find(Id);
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