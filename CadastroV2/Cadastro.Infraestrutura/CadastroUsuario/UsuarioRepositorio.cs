using Cadastro.Dominio.Arguments;
using Cadastro.Dominio.CadastroUsuario;
using Cadastro.Dominio.CadastroUsuario.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Cadastro.Infraestrutura.CadastroUsuario
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        public Usuario InserirUsuario(Usuario usuario)
        {
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"INSERT INTO usuario (login, senha, funcionarioid)
VALUES (@login, @senha, @funcionarioid)";

                    comando.Parameters.Add(CriarParametro(comando, "login", usuario.login));
                    comando.Parameters.Add(CriarParametro(comando, "senha", usuario.senha));
                    comando.Parameters.Add(CriarParametro(comando, "funcionarioid", usuario.funcionarioid));

                    if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                    comando.ExecuteNonQuery(); //ExecuteNonQuery quantidade de registro afetados pelo comando

                    comando.Connection.Close();
                }
            }

            return usuario;
        }

        public IEnumerable<Usuario> ListarUsuario()
        {
            var usuario_retorno = new List<Usuario>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"select u.usuarioid, u.login, 
u.senha, u.status, f.funcionarioid, f.nome, f.registro, f.telefone
from usuario u left join funcionario f on u.funcionarioid = f.funcionarioid";

                    if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        usuario_retorno.Add(new Usuario
                        {
                            usuarioid = (int)reader["usuarioid"],
                            login = (string)reader["login"],
                            senha = (string)reader["senha"],
                            funcionarioid = (int)reader["funcionarioid"],
                            nome = (string)reader["nome"],
                            registro = (string)reader["registro"],
                            telefone = (string)reader["telefone"],
                            status = (int)reader["status"]
                        });
                    }
                }
            }
            return usuario_retorno;
        }

        public Usuario ListarUsuarioPeloId(int id)
        {
            Usuario usuario = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"select u.usuarioid, u.login, 
u.senha, u.status, f.funcionarioid, f.nome, f.registro, f.telefone
from usuario u left join funcionario f on u.funcionarioid = f.funcionarioid
where usuarioid = @usuarioid" ;

                    comando.Parameters.Add(CriarParametro(comando, "usuarioid", id));
                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            usuarioid = (int)reader["usuarioid"],
                            login = (string)reader["login"],
                            senha = (string)reader["senha"],
                            funcionarioid = (int)reader["funcionarioid"],
                            nome = (string)reader["nome"],
                            registro = (string)reader["registro"],
                            telefone = (string)reader["telefone"],
                            status = (int)reader["status"]
                        };
                    }
                }
            }

            return usuario;
        }

        public void DeletarUsuario(int id)
        {
            Usuario usuario = ListarUsuarioPeloId(id);

            if (usuario != null)
            {
                int _id = usuario.usuarioid;

                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = @"Delete from Usuario where (usuarioid = @usuarioid)";

                        comando.Parameters.Add(CriarParametro(comando, "usuarioid", _id));

                        if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                        comando.ExecuteNonQuery(); //ExecuteNonQuery quantidade de registro afetados pelo comando
                        comando.Connection.Close();
                    }
                }
            }
        }

        public void AlterarUsuario(Usuario usuario)
        {

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"UPDATE usuario
SET login = @login,
senha = @senha,
funcionarioid = @funcionarioid
where usuarioid = @usuarioid";

                    comando.Parameters.Add(CriarParametro(comando, "login", usuario.login));
                    comando.Parameters.Add(CriarParametro(comando, "senha", usuario.senha));
                    comando.Parameters.Add(CriarParametro(comando, "funcionarioid", usuario.funcionarioid));

                    comando.Parameters.Add(CriarParametro(comando, "usuarioid", usuario.usuarioid));

                    if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                    comando.ExecuteNonQuery(); //ExecuteNonQuery quantidade de registro afetados pelo comando

                    comando.Connection.Close();
                }
            }
        }

        protected DbParameter CriarParametro(DbCommand cmd, string nome, object valor)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.ParameterName = nome;
            if (valor != null)
            {
                dbParameter.Value = valor;
            }
            else
            {
                dbParameter.Value = DBNull.Value;
            }

            return dbParameter;

        }

        public string Autenticar(Autenticarusuario request)
        {
            throw new NotImplementedException();
        }
    }
}
