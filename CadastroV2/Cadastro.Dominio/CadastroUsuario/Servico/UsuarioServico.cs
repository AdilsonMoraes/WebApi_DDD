﻿using Cadastro.Dominio.Arguments;
using Cadastro.Dominio.CadastroUsuario.Repositorio;
using Cadastro.Dominio.Helper;
using System.Collections.Generic;


namespace Cadastro.Dominio.CadastroUsuario.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioServico(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void InserirUsuario(Usuario usuario)
        {
            usuario.senha = CriptoHelper.HashMD5(usuario.senha);
            _repositorio.InserirUsuario(usuario);
        }

        public IEnumerable<Usuario> ListarUsuario()
        {
            return _repositorio.ListarUsuario();
        }

        public Usuario ListarUsuarioPeloId(int id)
        {

            return _repositorio.ListarUsuarioPeloId(id);
        }

        public void DeletarUsuario(int id)
        {
            _repositorio.DeletarUsuario(id);
        }

        public void AlterarUsuario(Usuario usuario)
        {
            usuario.senha = CriptoHelper.HashMD5(usuario.senha);
            _repositorio.AlterarUsuario(usuario);
        }

        public string Autenticar(Autenticarusuario request)
        {
            //if (request == null)
            //{
            //    throw new Exception("Autenticação zuada");
            //}

            //if (string.IsNullOrEmpty(request.login))
            //{
            //    throw new Exception("Autenticação zuada");
            //}

            //if (string.IsNullOrEmpty(request.senha))
            //{
            //    throw new Exception("Autenticação zuada");
            //}

            var response = _repositorio.Autenticar(request);
            return response;

        }      
    }
}
