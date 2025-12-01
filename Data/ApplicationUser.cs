using Microsoft.AspNetCore.Identity;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public int? InstitucionId { get; set; }
    public Institucion? Institucion { get; set; }

    public int? PeriodistaId { get; set; }
    public Periodista? Periodistas { get; set; }
    public string? NombreCompleto { get; set; }
}


