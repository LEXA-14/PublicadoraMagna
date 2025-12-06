using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class Periodista
{
    [Key]
    public int PeriodistaId { get; set; }
    [Required(ErrorMessage = "El nombre completo es requerido")]
    public string Nombres { get; set; }
    public bool EsActivo { get; set; } = true;
    //public string Email { get; set; }

    //implementar pago al periodista mas adelante
    [Range(1, double.MaxValue, ErrorMessage = "La tarifa base debe ser mayor a 0")]
    public decimal TarifaBase { get; set; }

    public DateTime FechaRegistro { get; set; }

    public decimal Balance { get; set; }

    public List<Articulo> Articulos { get; set; } = new();
    }



