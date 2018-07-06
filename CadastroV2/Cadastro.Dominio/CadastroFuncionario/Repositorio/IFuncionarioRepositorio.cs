using System.Collections.Generic;

namespace Cadastro.Dominio.CadastroFuncionario.Repositorio
{
    public interface IFuncionarioRepositorio
    {
        IEnumerable<Funcionario> ListarFuncionario();
        Funcionario ListarFuncionarioPeloId(int id);
        void InserirFuncionario(Funcionario funcionario);
        void DeletarFuncionario(int id);
        void AlterarFuncionario(Funcionario funcionario);
    }
}
