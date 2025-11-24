namespace PublicadoraMagna.Model;

public class ArticuloServicioPromocional
{
  
        public int ArticuloServicioPromocionalId { get; set; }

        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        public int ServicioPromocionalId { get; set; }
        public ServicioPromocional ServicioPromocional { get; set; }

        public decimal PrecioAplicado { get; set; }
        public DateTime FechaAplicacion { get; set; }
    
}
