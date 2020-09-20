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
            _jogo = jogo;
        }

        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = "admin")]
        public ActionResult<Jogo> Inserir([FromBody] Jogo model)
        {
            try
            {
                var jogo = _jogo.Inserir(model);
                if (jogo == null)
                    return BadRequest(new { message = "Jogo não encontrado!" });

                if (model.Id > 0)
                    return Ok(new { message = "Jogo atualizado com sucesso!", sucess = true, itens = jogo });
                else
                    return Ok(new { message = "Jogo cadastrado com sucesso!", sucess = true, itens = jogo });

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

                var removido = _jogo.Remove(Id);
                if (removido == false)
                    return BadRequest(new { message = "Jogo não encontrado!" });

                return Ok(new { message = "Jogo removido com sucesso!", sucess = removido });

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
                var jogos = _jogo.Get();
                if (jogos.Count == 0)
                    return Ok(new { message = "Nenhum jogo cadastrado!", sucess = false });

                return Ok(new { message = "Jogos retornados com sucesso!", sucess = true, itens = jogos });

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
                    return BadRequest(new { message = "Não foi encontrado jogo com esse Id", sucess = true });

                return Ok(new { message = "Jogo retornada com sucesso!", sucess = true, itens = jogos });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }
    }
}