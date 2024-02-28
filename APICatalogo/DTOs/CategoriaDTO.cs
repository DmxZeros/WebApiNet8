using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace APICatalogo.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        
        [Required]
        [StringLength(300)]
        public String? ImagemUrl { get; set; }
    }
}
