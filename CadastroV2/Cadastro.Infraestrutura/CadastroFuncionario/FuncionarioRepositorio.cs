using Cadastro.Dominio.CadastroFuncionario;
using Cadastro.Dominio.CadastroFuncionario.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Cadastro.Infraestrutura.CadastroFuncionario
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        public void AlterarFuncionario(Funcionario funcionario)
        {
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"UPDATE funcionario
SET registro = @registro,
nome = @nome,
telefone = @telefone
where funcionarioid = @funcionarioid";

                    comando.Parameters.Add(CriarParametro(comando, "registro", funcionario.registro));
                    comando.Parameters.Add(CriarParametro(comando, "nome", funcionario.nome));
                    comando.Parameters.Add(CriarParametro(comando, "telefone", funcionario.telefone));

                    comando.Parameters.Add(CriarParametro(comando, "funcionarioid", funcionario.funcionarioid));

                    if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                    comando.ExecuteNonQuery(); //ExecuteNonQuery quantidade de registro afetados pelo comando

                    comando.Connection.Close();
                }
            }
        }

        public void DeletarFuncionario(int id)
        {
            Funcionario funcionario = ListarFuncionarioPeloId(id);

            if (funcionario != null)
            {
                int _id = funcionario.funcionarioid;

                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = @"Delete from funcionario where funcionarioid = @funcionarioid";

                        comando.Parameters.Add(CriarParametro(comando, "funcionarioid", _id));

                        if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                        comando.ExecuteNonQuery(); 
                        comando.Connection.Close();
                    }
                }
            }
        }

        public void InserirFuncionario(Funcionario funcionario)
        {
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"INSERT INTO funcionario (registro, nome, telefone)
VALUES (@registro, @nome, @telefone)";

                    comando.Parameters.Add(CriarParametro(comando, "registro", funcionario.registro));
                    comando.Parameters.Add(CriarParametro(comando, "nome", funcionario.nome));
                    comando.Parameters.Add(CriarParametro(comando, "telefone", funcionario.telefone));

                    if (comando.Connection.State != ConnectionState.Open) { comando.Connection.Open(); }

                    comando.ExecuteNonQuery(); //ExecuteNonQuery quantidade de registro afetados pelo comando

                    comando.Connection.Close();
                }
            }
        }

        public IEnumerable<Funcionario> ListarFuncionario()
        {
            var funcionario = new List<Funcionario>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ToString();
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select funcionarioid, registro, nome,  telefone from funcionario");
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        funcionario.Add(new Funcionario
                        {
                            funcionarioid = reader.GetInt32(0),
                            registro = reader.GetString(1),
                            nome = reader.GetString(2),
                            telefone = reader.GetString(3)
                        });
                    }
                }
            }
            return funcionario;
        }

        public Funcionario ListarFuncionarioPeloId(int id)
        {
            Funcionario funcionario = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = @"select funcionarioid, registro, nome,  telefone
from funcionario 
where funcionarioid = @funcionarioid";

                    comando.Parameters.Add(CriarParametro(comando, "funcionarioid", id));

                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        funcionario = new Funcionario
                        {
                            funcionarioid = reader.GetInt32(0),
                            nome = reader.GetString(1),
                            registro = reader.GetString(2),
                            telefone = reader.GetString(3)
                        };
                    }
                }
            }

            return funcionario;
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
