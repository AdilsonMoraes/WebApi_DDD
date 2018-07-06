using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio.CadastroFuncionario.Servico
{
    public interface IFuncionarioServico
    {
        IEnumerable<Funcionario> ListarFuncionario();
        Funcionario ListarFuncionarioPeloId(int id);
        void InserirFuncionario(Funcionario funcionario);
        void DeletarFuncionario(int id);
        void AlterarFuncionario(Funcionario funcionario);
    }
}
