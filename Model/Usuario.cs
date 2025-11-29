using Microsoft.AspNetCore.Identity;

namespace PublicadoraMagna.Model;


public class Usuario : IdentityUser
{
    public string Nombre { get; set; }            
    public string RolSistema { get; set; }         
    public int? InstitucionId { get; set; }      
    public DateTime FechaRegistro { get; set; }    
    public Institucion Institucion { get; set; }
}