using System.Collections.Generic;
using System.Linq;
using EmprestaGames.Api.Models;

namespace EmprestaGames.Api.Respositories
{
    public class UsuarioRepository
    {
        public static Usuario Get(string nome, string senha)
        {
            var users = new List<Usuario>();
            users.Add(new Usuario { Id = 1, Nome = "user", Senha = "user", Role = "user" });
            users.Add(new Usuario { Id = 2, Nome = "admin", Senha = "admin", Role = "admin" });

            return users.Where(x => x.Nome.ToLower() == nome.ToLower() && x.Senha == senha).FirstOrDefault();
        }
    }
}