using Cadastro.Dominio.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio.CadastroUsuario.Repositorio
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Usuario> ListarUsuario();
        Usuario ListarUsuarioPeloId(int id);
        Usuario InserirUsuario(Usuario item);
        void DeletarUsuario(int id);
        void AlterarUsuario(Usuario item);
        string Autenticar(Autenticarusuario request); //vai retornar o token
    }
}
