using System.Collections.Generic;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestaGames.Api.Controllers
{
    [Route("v1/emprestimo")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private IEmprestimoRepository _emprestimo;


        public EmprestimoController(IEmprestimoRepository emprestimo)
        {
            _emprestimo = emprestimo;
        }

        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = "admin")]
        public ActionResult<Emprestimo> Inserir([FromBody] Emprestimo model)
        {
            try
            {
                var pessoa = _emprestimo.Inserir(model);
                if (pessoa == null)
                    return BadRequest(new { message = "Jogo não cadastrada" });

                return Ok(new { message = "Jogo cadastrada com sucesso!", sucess = true, itens = pessoa });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpPost]
        [Route("remover")]
        [Authorize(Roles = "admin")]
        public ActionResult<bool> Remover(int Id)
        {
            try
            {
                var removido = _emprestimo.Remove(Id);
                if (removido == false)
                    return BadRequest(new { message = "Jogo não foi encontrada!" });

                return Ok(new { message = "Jogo removida com sucesso!", sucess = removido });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpPost]
        [Route("devolver")]
        [Authorize(Roles = "admin")]
        public ActionResult<bool> Devolver(int Id)
        {
            try
            {
                var removido = _emprestimo.Devolver(Id);
                if (removido == false)
                    return BadRequest(new { message = "Jogo não foi encontrada!" });

                return Ok(new { message = "Jogo devolvido com sucesso!", sucess = removido });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "user, admin")]
        public ActionResult<List<Emprestimo>> Get()
        {
            try
            {
                var pessoas = _emprestimo.Get();
                if (pessoas.Count == 0)
                    return Ok(new { message = "Não foi encontrado registros!", sucess = false });

                return Ok(new { message = "Jogo retornada com sucesso!", sucess = true, itens = pessoas });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("soemprestados")]
        [Authorize(Roles = "user, admin")]
        public ActionResult<List<Emprestimo>> GetSoEmprestado()
        {
            try
            {
                var pessoas = _emprestimo.GetSoEmprestado();
                if (pessoas.Count == 0)
                    return Ok(new { message = "Não foi encontrado registros!", sucess = false });

                return Ok(new { message = "Jogo retornada com sucesso!", sucess = true, itens = pessoas });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("getid")]
        [Authorize(Roles = "admin")]
        public ActionResult<Emprestimo> GetId(int Id)
        {
            try
            {
                var jogos = _emprestimo.GetId(Id);
                if (jogos == null)
                    return BadRequest(new { message = "Não foi encontrado jogo com esse Id" });

                return Ok(new { message = "Jogo retornada com sucesso!", sucess = true, itens = jogos });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }
    }
}