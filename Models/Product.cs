using System;
using System.ComponentModel.DataAnnotations;

namespace Shop_v3._1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(60, ErrorMessage = "Este campo precisa ter no máximo {0}.")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter no mínimo {0}.")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "")]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "")]
        public int CategoryId { get; set; }

        /* EF Relation - Propriedade de Referência*/
        public Category Category { get; set; }
    }
}
