using Cadastro.Dominio.CadastroFuncionario.Repositorio;
using Cadastro.Dominio.CadastroFuncionario.Servico;
using Cadastro.Dominio.CadastroUsuario.Repositorio;
using Cadastro.Dominio.CadastroUsuario.Servico;
using Cadastro.Infraestrutura.CadastroFuncionario;
using Cadastro.Infraestrutura.CadastroUsuario;
using SimpleInjector;

namespace Cadastro.IoC
{
    public static class BootStrap
    {
        public static void RegistrarServico(Container container)
        {
            CadastroUsuario(container);
            CadastroFuncionario(container);
        }

        private static void CadastroUsuario(Container container)
        {
            container.Register<IUsuarioServico, UsuarioServico>();
            container.Register<IUsuarioRepositorio, UsuarioRepositorio>();
        }

        private static void CadastroFuncionario(Container container)
        {
            container.Register<IFuncionarioServico, FuncionarioServico>();
            container.Register<IFuncionarioRepositorio, FuncionarioRepositorio>();
        }

    }
}
