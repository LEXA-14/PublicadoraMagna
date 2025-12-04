using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;


public class EncargoServicioPromocional
{
    [Key]
    public int EncargoServicioPromocionalId { get; set; }

  
    public int EncargoArticuloId { get; set; }
    public EncargoArticulo EncargoArticulo { get; set; } = null!;

    public int ServicioPromocionalId { get; set; }
    public ServicioPromocional ServicioPromocional { get; set; } = null!;

    public decimal PrecioAplicado { get; set; }
    public DateTime FechaAplicacion { get; set; }
}

