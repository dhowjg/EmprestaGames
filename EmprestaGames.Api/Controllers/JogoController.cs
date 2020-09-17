using System.Collections.Generic;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestaGames.Api.Controllers
{
    [Route("v1/jogo")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private IJogoRepository _jogo;


        public JogoController(IJogoRepository jogo)
        {
            jogo = _jogo;
        }

        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = "admin")]
        public ActionResult<Jogo> Inserir([FromBody] Jogo model)
        {
            try
            {
                var pessoa = _jogo.Inserir(model);
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
        public ActionResult<bool> Remover([FromBody] Jogo model)
        {
            try
            {
                var removido = _jogo.Remove(model);
                if (removido == false)
                    return BadRequest(new { message = "Jogo não foi encontrada!" });

                return Ok(new { message = "Jogo removida com sucesso!", sucess = removido });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "user, admin")]
        public ActionResult<List<Jogo>> Get()
        {
            try
            {
                var pessoas = _jogo.Get();
                if (pessoas.Count == 0)
                    return BadRequest(new { message = "Não foi encontrados jogos cadastradas!" });

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
        public ActionResult<Jogo> GetId(int Id)
        {
            try
            {
                var jogos = _jogo.GetId(Id);
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