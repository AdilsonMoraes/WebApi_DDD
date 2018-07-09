using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFirst.Models
{
    public class Usuario
    {
        public int usuarioid { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public int status { get; set; }
        public int funcionarioid { get; set; }

        [ForeignKey("funcionarioid")]
        public Funcionario Funcionario { get; set; }
    }
}
