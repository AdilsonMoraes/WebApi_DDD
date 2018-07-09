using Cadastro.Dominio.CadastroFuncionario;

namespace Cadastro.Dominio.CadastroUsuario
{
    public class Usuario : Funcionario 
    {

        public int usuarioid { get; set; }

        public string login { get; set; }

        public string senha { get; set; }

        public int status { get; set; }
    }
}
