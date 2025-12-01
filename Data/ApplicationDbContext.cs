using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> Usuario { get; set; }
    public DbSet<Institucion> Instituciones { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<PagoInstitucion> PagosInstitucion { get; set; }
    public DbSet<DetallePagoInstitucion> DetallesPagosInstitucion { get; set; }
    public DbSet<Periodista> Periodistas { get; set; }
    public DbSet<PagoPeriodista> PagosPeriodistas { get; set; }
    public DbSet<DetallePagoPeriodista> DetallesPagosPeriodistas { get; set; }
    public DbSet<ServicioPromocional> ServiciosPromocionales { get; set; }


    public DbSet<ArticuloServicioPromocionales> ArticuloServicioPromocional { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<ApplicationUser>(entity =>
        {

            entity.HasOne(u => u.Institucion)
                .WithMany(i => i.Usuarios)
                .HasForeignKey(u => u.InstitucionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);


            entity.HasOne(u => u.Periodistas)
                .WithMany()
                .HasForeignKey(u => u.PeriodistaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.Property(u => u.NombreCompleto)
                .HasMaxLength(200);
        });



        modelBuilder.Entity<Institucion>(entity =>
        {
            entity.Property(i => i.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(i => i.Rnc)
                .HasMaxLength(50);

            entity.Property(i => i.EmailAdmin)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(i => i.CorreoContacto)
                .HasMaxLength(256);

            entity.Property(i => i.Telefono)
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Periodista>(entity =>
        {
            entity.Property(p => p.Nombres)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.TarifaBase)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.EsActivo)
                .HasDefaultValue(true);
        });


        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.Property(a => a.Titulo)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(a => a.Estado)
                .HasConversion<int>();


            entity.HasOne(a => a.Categoria)
                .WithMany()
                .HasForeignKey(a => a.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            entity.HasOne(a => a.Institucion)
                .WithMany(i => i.Articulos)
                .HasForeignKey(a => a.InstitucionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);


            entity.HasOne(a => a.Periodista)
                .WithMany(p => p.Articulos)
                .HasForeignKey(a => a.PeriodistaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.PrecioBase)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<ServicioPromocional>(entity =>
        {
            entity.Property(s => s.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(s => s.Precio)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<ArticuloServicioPromocionales>(entity =>
        {
            entity.HasOne(asp => asp.Articulo)
                .WithMany(a => a.ServiciosPromocionales)
                .HasForeignKey(asp => asp.ArticuloId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(asp => asp.ServicioPromocional)
                .WithMany()
                .HasForeignKey(asp => asp.ServicioPromocionalId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(asp => asp.PrecioAplicado)
                .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<PagoInstitucion>(entity =>
        {
            entity.HasOne(p => p.Institucion)
                .WithMany()
                .HasForeignKey(p => p.InstitucionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.Monto)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Estado)
                .HasMaxLength(50);
        });
        modelBuilder.Entity<DetallePagoInstitucion>(entity =>
        {
            entity.HasOne(d => d.Pago)
                .WithMany(p => p.DetallesPagos)
                .HasForeignKey(d => d.PagoInstitucionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Articulo)
                .WithMany()
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(d => d.Monto)
                .HasColumnType("decimal(18,2)");
        });


        modelBuilder.Entity<PagoPeriodista>(entity =>
        {
            entity.HasOne(p => p.Periodista)
                .WithMany()
                .HasForeignKey(p => p.PeriodistaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.Total)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Estado)
                .HasMaxLength(50);
        });
        modelBuilder.Entity<DetallePagoPeriodista>(entity =>
        {
            entity.HasOne(d => d.Pago)
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.PagoPeriodistaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Articulo)
                .WithMany()
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(d => d.Monto)
                .HasColumnType("decimal(18,2)");
        });
    }

}
