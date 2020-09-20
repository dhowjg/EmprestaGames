using System.Collections.Generic;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestaGames.Api.Controllers
{
    [Route("v1/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private IPessoaRespository _pessoa;


        public PessoaController(IPessoaRespository pessoa)
        {
            _pessoa = pessoa;
        }

        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = "admin")]
        public ActionResult<Pessoa> Inserir([FromBody] Pessoa model)
        {
            try
            {
                var pessoa = _pessoa.Inserir(model);
                if (pessoa == null)
                    return BadRequest(new { message = "Pessoa não encontrada!" });

                if (model.Id > 0)
                    return Ok(new { message = "Pessoa atualizada com sucesso!", sucess = true, itens = pessoa });
                else
                    return Ok(new { message = "Pessoa cadastrada com sucesso!", sucess = true, itens = pessoa });

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
                var removido = _pessoa.Remove(Id);
                if (removido == false)
                    return BadRequest(new { message = "Pessoa não encontrada!" });

                return Ok(new { message = "Pessoa removida com sucesso!", sucess = removido });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "user, admin")]
        public ActionResult<List<Pessoa>> Get()
        {
            try
            {
                var pessoas = _pessoa.Get();
                if (pessoas.Count == 0)
                    return Ok(new { message = "Nenhum pessoa cadastrada!", sucess = false });

                return Ok(new { message = "Pessoa retornada com sucesso!", sucess = true, itens = pessoas });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("getid")]
        [Authorize(Roles = "admin")]
        public ActionResult<Pessoa> GetId(int Id)
        {
            try
            {
                var pessoas = _pessoa.GetId(Id);
                if (pessoas == null)
                    return BadRequest(new { message = "Não foi encontrado pessoa com esse Id" });

                return Ok(new { message = "Pessoa retornada com sucesso!", sucess = true, itens = pessoas });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }
    }
}