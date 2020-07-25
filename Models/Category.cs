using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_v3._1.Models
{
    [Table("Categoria")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(60, ErrorMessage = "Este campo precisa ter no máximo {0}.")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo {0}.")]
        public string Title { get; set; }
    }
}
