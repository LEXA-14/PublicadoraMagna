namespace PublicadoraMagna.Model;

public class ServicioPromocional
{
  
        public int ServicioPromocionalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }              
        public DateTime FechaCreacion { get; set; }
    
}
