using System.Threading.Tasks;
using EmprestaGames.Api.Models;
using EmprestaGames.Api.Respositories;
using EmprestaGames.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestaGames.Api.Controllers
{
    [Route("v1/account")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticacao([FromBody] Usuario model)
        {
            try
            {
                var user = UsuarioRepository.Get(model.Nome, model.Senha);
                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                var token = TokenServices.GenerateToken(user);
                user.Senha = "";
                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (System.Exception erro)
            {
                return BadRequest(erro.Message.ToString());
            }

        }
    }
}