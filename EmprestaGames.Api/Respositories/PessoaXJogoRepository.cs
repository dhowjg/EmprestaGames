using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Response;

namespace EmprestaGames.Api.Respositories
{
    public class PessoaXJogoRepository : IPessoaXJogoRepository
    {
        public List<PessoaXJogosResponsecs> Get()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var itens = (from pxj in db.PessoasXJogos
                                 join jogo in db.Jogos on pxj.JogoId equals jogo.Id
                                 join pessoa in db.Pessoas on pxj.PessoaId equals pessoa.Id
                                 select new
                                 {
                                     pxj.Id,
                                     pxj.JogoId,
                                     pxj.PessoaId,
                                     nomejogo = jogo.Nome,
                                     nomepessoa = pessoa.Nome
                                 }).ToList();

                    var list = new List<PessoaXJogosResponsecs>();
                    foreach (var item in itens)
                    {
                        list.Add(new PessoaXJogosResponsecs
                        {
                            Id = item.Id,
                            JogoId = item.JogoId,
                            NomeJogo = item.nomejogo,
                            NomePessoa = item.nomepessoa,
                            PessoaId = item.PessoaId
                        });
                    }

                    return list;
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

        public bool Remove(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    if (Id == 0) return false;
                    var model = db.PessoasXJogos.Find(Id);
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