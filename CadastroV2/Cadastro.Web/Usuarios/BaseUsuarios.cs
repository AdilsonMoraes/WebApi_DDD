using Cadastro.Web.UsuariosModelo;
using System.Collections.Generic;

namespace Cadastro.Web.BaseUsr
{
    // simulacao de acesso a uma base de usuarios
    public static class BaseUsuarios
    {
        public static IEnumerable<UsuarioToken> UsuariosValidos()
        {
            return new List<UsuarioToken>
            {
                new UsuarioToken { Nome = "Adilson", Senha = "1234" },
                new UsuarioToken { Nome = "Adilson2", Senha = "5678" },
                new UsuarioToken { Nome = "ADilson3", Senha = "0912" }
            };
        }
    }
}