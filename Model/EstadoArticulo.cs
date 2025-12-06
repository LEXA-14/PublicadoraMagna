using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public enum EstadoArticulo
{
    Borrador = 0,
    Pendiente = 1,
    AprobadoInstitucion = 2, 
    AprobadoEditor = 3,
    Rechazado = 4,
    Enviado = 5,
    Pagado=6
}
