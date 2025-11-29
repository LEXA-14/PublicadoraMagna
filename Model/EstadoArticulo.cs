using System.ComponentModel.DataAnnotations;

namespace PublicadoraMagna.Model;

public enum EstadoArticulo
{
    Borrador = 0,

    Pendiente = 1,

    Aprobado = 2,

    Rechazado = 3,

    Enviado = 4
}
