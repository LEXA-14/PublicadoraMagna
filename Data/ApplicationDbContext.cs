using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
      
        public DbSet<PagoInstitucion> PagosInstitucion{ get; set; }
        public DbSet<DetallePagoInstitucion> DetallesPagosInstitucion { get; set; }
        public DbSet<Periodista> Periodistas { get; set; }
        public DbSet<PagoPeriodista> PagosPeriodistas { get; set; }
        public DbSet<DetallePagoPeriodista> DetallesPagosPeriodistas { get; set; }

      
        public DbSet<ArticuloServicioPromocional> ArticuloServicioPromocional { get; set; }
    }
}
