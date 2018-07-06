using Cadastro.Dominio.CadastroFuncionario.Repositorio;
using System;
using System.Collections.Generic;

namespace Cadastro.Dominio.CadastroFuncionario.Servico
{
    public class FuncionarioServico : IFuncionarioServico
    {
        private readonly IFuncionarioRepositorio _repositorio;

        public FuncionarioServico(IFuncionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void AlterarFuncionario(Funcionario funcionario)
        {
            _repositorio.AlterarFuncionario(funcionario);
        }

        public void DeletarFuncionario(int id)
        {
            _repositorio.DeletarFuncionario(id);
        }

        public void InserirFuncionario(Funcionario funcionario)
        {
            _repositorio.InserirFuncionario(funcionario);
        }

        public IEnumerable<Funcionario> ListarFuncionario()
        {
            return _repositorio.ListarFuncionario();
        }

        public Funcionario ListarFuncionarioPeloId(int id)
        {
            return _repositorio.ListarFuncionarioPeloId(id);
        }
    }
}
