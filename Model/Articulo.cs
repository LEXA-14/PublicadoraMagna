using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public class Articulo
{
        [Key]
        public int ArticuloId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Resumen { get; set; }
        public string Contenido { get; set; }

        public string AutorId { get; set; }            

        public int? InstitucionId { get; set; }       
        public Institucion Institucion { get; set; }

       
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public EstadoArticulo Estado { get; set; }

    
        public List<ArticuloServicioPromocional> ServiciosPromocionales { get; set; } = new();

        public List<string> Imagenes { get; set; } = new();

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaPublicacion { get; set; }

        
        public decimal TotalPromocional => ServiciosPromocionales?.Sum(s => s.PrecioAplicado) ?? 0m;
        public decimal TotalAPagar => (Categoria?.PrecioBase ?? 0m) + TotalPromocional;
    }



