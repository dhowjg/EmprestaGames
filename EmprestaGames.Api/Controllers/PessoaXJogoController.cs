using System.Collections.Generic;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestaGames.Api.Controllers
{
    [Route("v1/pessoaxjogo")]
    [ApiController]
    public class PessoaXJogoController : ControllerBase
    {
        private IPessoaXJogoRepository  _pessoaxjogo;


        public PessoaXJogoController(IPessoaXJogoRepository pessoaxjogo)
        {
            _pessoaxjogo = pessoaxjogo;
        }

        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = "admin")]
        public ActionResult<PessoaXJogo> Inserir([FromBody] PessoaXJogo model)
        {
            try
            {
                var pessoa = _pessoaxjogo.Inserir(model);
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
        public ActionResult<bool> Remover(int id)
        {
            try
            {
                var removido = _pessoaxjogo.Remove(id);
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
        public ActionResult<List<dynamic>> Get()
        {
            try
            {
                var pessoas = _pessoaxjogo.Get();
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
        public ActionResult<PessoaXJogo> GetId(int Id)
        {
            try
            {
                var jogos = _pessoaxjogo.GetId(Id);
                if (jogos == null)
                    return BadRequest(new { message = "Não foi encontrado jogo com esse Id" });

                return Ok(new { message = "Jogo retornada com sucesso!", sucess = true, itens = jogos });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("getpessoasdonos")]
        [Authorize(Roles = "user, admin")]
        public ActionResult<Pessoa> GetJogo(int JogoId)
        {
            try
            {
                var pessoas = _pessoaxjogo.GetPessoa(JogoId);
                if (pessoas == null)
                    return BadRequest(new { message = "Não foi encontrado jogos para essa pessoa" });

                return Ok(new { message = "Pessoa retornada com sucesso!", sucess = true, itens = pessoas });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }

        [HttpGet]
        [Route("jogonaoemprestado")]
        [Authorize(Roles = "user, admin")]
        public ActionResult<Pessoa> GetJogoNaoEmprestado()
        {
            try
            {
                var pessoas = _pessoaxjogo.GetJogosNaoEmprestados();
                if (pessoas == null)
                    return BadRequest(new { message = "Todos os jogos estão emprestados!" });

                return Ok(new { message = "Jogos retornada com sucesso!", sucess = true, itens = pessoas });

            }
            catch (System.Exception erro)
            {
                return BadRequest(new { message = erro.Message.ToString(), sucess = false });
            }
        }
    }
}