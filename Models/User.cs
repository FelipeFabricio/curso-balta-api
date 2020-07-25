using System.ComponentModel.DataAnnotations;

namespace Shop_v3._1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Este campo precisa ter no máximo {0}.")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo {0}.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Este campo precisa ter no máximo {0}.")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo {0}.")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
