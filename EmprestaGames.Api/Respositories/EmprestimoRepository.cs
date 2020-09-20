using System;
using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Response;
using Microsoft.EntityFrameworkCore;

namespace EmprestaGames.Api.Respositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        public bool Devolver(int Id)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    if (Id == 0) return false;
                    var model = db.Emprestimos.Find(Id);
                    model.DataDevolvido = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public List<EmprestimoResponse> Get()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var itens = (from emp in db.Emprestimos
                                 join jogo in db.Jogos on emp.JogoId equals jogo.Id
                                 join pessoaDona in db.Pessoas on emp.PessoaDonoJogoId equals pessoaDona.Id
                                 join pessoaEmprestada in db.Pessoas on emp.PessoaEmprestadaId equals pessoaEmprestada.Id
                                 select new
                                 {
                                     emp.Id,
                                     emp.JogoId,
                                     emp.PessoaEmprestadaId,
                                     nomejogo = jogo.Nome,
                                     dequem = pessoaDona.Nome,
                                     praquem = pessoaEmprestada.Nome,
                                     emp.DataEmprestimo,
                                     emp.DataDevolvido
                                 }).ToList();

                    var list = new List<EmprestimoResponse>();
                    foreach (var item in itens)
                    {
                        list.Add(new EmprestimoResponse
                        {
                            Id = item.Id,
                            Jogo = item.nomejogo,
                            DeQuem = item.dequem,
                            PraQuem = item.praquem,
                            DataEmprestimo = item.DataEmprestimo,
                            DataDevolvido = item.DataDevolvido
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

        public Emprestimo GetId(int Id)
        {
            throw new NotImplementedException();
        }

        public List<EmprestimoResponse> GetSoEmprestado()
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var itens = (from emp in db.Emprestimos
                                 join jogo in db.Jogos on emp.JogoId equals jogo.Id
                                 join pessoaDona in db.Pessoas on emp.PessoaDonoJogoId equals pessoaDona.Id
                                 join pessoaEmprestada in db.Pessoas on emp.PessoaEmprestadaId equals pessoaEmprestada.Id
                                 where emp.DataDevolvido == null
                                 select new
                                 {
                                     emp.Id,
                                     emp.JogoId,
                                     emp.PessoaEmprestadaId,
                                     nomejogo = jogo.Nome,
                                     dequem = pessoaDona.Nome,
                                     praquem = pessoaEmprestada.Nome,
                                     emp.DataEmprestimo,
                                     emp.DataDevolvido
                                 }).ToList();

                    var list = new List<EmprestimoResponse>();
                    foreach (var item in itens)
                    {
                        list.Add(new EmprestimoResponse
                        {
                            Id = item.Id,
                            Jogo = item.nomejogo,
                            DeQuem = item.dequem,
                            PraQuem = item.praquem,
                            DataEmprestimo = item.DataEmprestimo,
                            DataDevolvido = item.DataDevolvido
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

        public Emprestimo Inserir(Emprestimo model)
        {
            try
            {
                using (EGContext db = new EGContext())
                {
                    var emprestimo = model;
                    emprestimo.DataEmprestimo = DateTime.Now;
                    db.Emprestimos.Add(emprestimo);                    
                    db.SaveChanges();

                    return emprestimo;
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
                    var model = db.Emprestimos.Find(Id);
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