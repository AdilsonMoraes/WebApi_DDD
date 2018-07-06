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
            var usuario = new List<Usuario>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from usuario");
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.Add(new Usuario
                        {
                            usuarioid = (int)reader["usuarioid"],
                            login = (string)reader["login"],
                            senha = (string)reader["senha"]
                        });
                    }
                }
            }
            return usuario;
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
                    comando.CommandText = @"select usuarioid, login, senha 
from Usuario 
where usuarioid = @usuarioid";

                    comando.Parameters.Add(CriarParametro(comando, "usuarioid", id));

                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            usuarioid = reader.GetInt32(0),
                            login = reader.GetString(1),
                            senha = reader.GetString(2)
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

    }
}
