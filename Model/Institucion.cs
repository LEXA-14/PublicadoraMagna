using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class Institucion
{
    [Key]
    public int InstitucionId { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string Rnc { get; set; }

    public string EmailAdmin { get; set; }
    public string CorreoContacto { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaRegistro { get; set; }
    public List<Articulo> Articulos { get; set; } = new();


    public ICollection<Usuario> Usuarios { get; set; }
}
