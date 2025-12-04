using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class EncargoArticulo
{
    [Key]
    public int EncargoArticuloId { get; set; }

    public int InstitucionId { get; set; }
    public Institucion Institucion { get; set; } = null!;

    public int PeriodistaId { get; set; }
    public Periodista Periodista { get; set; } = null!;
    public string TituloSugerido { get; set; } = string.Empty;
    public string? DescripcionEncargo { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public string? ComentarioRechazo { get; set; }
    public List<EncargoServicioPromocional> ServiciosPromocionales { get; set; } = new();


    public EstadoArticulo Estado { get; set; }

 
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaRespuestaPeriodista { get; set; }
    public DateTime? FechaEnvioInstitucion { get; set; }
    public DateTime? FechaRespuestaInstitucion { get; set; }

    
    public string? ComentarioPeriodista { get; set; }
    public string? ComentarioInstitucion { get; set; }

    public int? ArticuloId { get; set; }
    public Articulo? Articulo { get; set; }

    // Totales
    public decimal TotalPromocional => ServiciosPromocionales?.Sum(s => s.PrecioAplicado) ?? 0m;
    public decimal TotalAPagar => (Categoria?.PrecioBase ?? 0m) + TotalPromocional + (Periodista?.TarifaBase ?? 0m);
}
