using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio.CadastroUsuario
{
    public class Usuario
    {
        public int usuarioid { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string funcionarioid { get; set; }
    }
}
