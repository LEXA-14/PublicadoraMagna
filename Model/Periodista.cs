using Microsoft.Win32;

namespace PublicadoraMagna.Model
{
    public class Periodista
    {
            public int PeriodistaId { get; set; }

            public string Nombres { get; set; }

        //implementar pago al periodista mas adelante
        public decimal TarifaBase { get; set; }


        public List<Articulo> Articulos { get; set; } = new();
        }

   }

