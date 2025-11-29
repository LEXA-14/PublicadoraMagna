using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class Categoria
{
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public decimal PrecioBase { get; set; }
        public DateTime FechaCreacion { get; set; }
    
}
