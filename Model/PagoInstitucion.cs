using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class PagoInstitucion
{
    [Key]
   public int PagoId { get; set; }
   public int InstitucionId { get; set; }
   public Institucion Institucion { get; set; }
   public DateTime FechaPago { get; set; }
   public decimal Monto { get; set; }
   public string Estado { get; set; }
   public List<DetallePagoInstitucion> DetallesPagos { get; set; } = new();
    
}

