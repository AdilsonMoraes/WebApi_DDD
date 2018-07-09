using Cadastro.Dominio.Arguments;
using System.Collections.Generic;

namespace Cadastro.Dominio.CadastroUsuario.Servico
{
    public interface IUsuarioServico
    {
        IEnumerable<Usuario> ListarUsuario();
        Usuario ListarUsuarioPeloId(int id);
        void InserirUsuario(Usuario item);
        void DeletarUsuario(int id);
        void AlterarUsuario(Usuario item);
        string Autenticar(Autenticarusuario request); //vai retornar o token
    }
}
