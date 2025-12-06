using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class PeriodistaService(IDbContextFactory<ApplicationDbContext> dbFactory,UserManager<ApplicationUser> userManager)
{
    
    public async Task<bool> CrearConUsuario(
     Periodista periodista,
     string email,
     string password)
    {

        var usuarioExiste = await userManager.FindByEmailAsync(email);
        if (usuarioExiste != null)
            return false;

        if (string.IsNullOrWhiteSpace(periodista.Nombres))
            return false;

        if (periodista.TarifaBase < 0)
            return false;


        periodista.FechaRegistro = DateTime.Now;
        periodista.EsActivo = true;

        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Periodistas.Add(periodista);
        var guardado = await contexto.SaveChangesAsync() > 0;

        if (!guardado)
            return false;

        await contexto.Entry(periodista).ReloadAsync();

        // Crear el usuario
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            NombreCompleto = periodista.Nombres,
            PeriodistaId = periodista.PeriodistaId,
            EmailConfirmed = true,
            //FechaRegistro = DateTime.Now
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return false;

        // Asignar rol de Periodista
        await userManager.AddToRoleAsync(user, AppRoles.Periodista);

        return true;

    }




    public async Task<bool> Guardar(Periodista periodista)
    {
       
        if (string.IsNullOrWhiteSpace(periodista.Nombres))
            throw new Exception("El nombre del periodista es requerido");
        if (periodista.TarifaBase < 0)
            throw new Exception("La tarifa no puede ser negativa");

        if (!await Existe(periodista.PeriodistaId))
        {
            return await Insertar(periodista);
        }
        else
        {
            return await Modificar(periodista);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var entidad = await contexto.Periodistas
            .Include(p => p.Articulos)
            .FirstOrDefaultAsync(p => p.PeriodistaId == id);

        if (entidad == null) return false;

        if (entidad.Articulos != null && entidad.Articulos.Any())
            throw new Exception("No se puede eliminar un periodista que tiene artículos publicados");

        contexto.Periodistas.Remove(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

 
    public async Task<bool> CambiarEstado(int id, bool esActivo)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var periodista = await contexto.Periodistas.FindAsync(id);

        if (periodista == null) return false;

     
        periodista.EsActivo = esActivo;
        contexto.Periodistas.Update(periodista);
        return await contexto.SaveChangesAsync() > 0;
    }

   
    public async Task<Periodista?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas
            .Include(p => p.Articulos)
            .FirstOrDefaultAsync(p => p.PeriodistaId == id);
    }

    public async Task<List<Periodista>> Listar()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas
            .Include(p => p.Articulos)
            .ToListAsync();
    }

    public async Task<List<Periodista>> ConsultarPorFecha(DateTime fechaDesde, DateTime fechaHasta)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas
            .Where(p => p.FechaRegistro >= fechaDesde && p.FechaRegistro <= fechaHasta)
            .ToListAsync();
    }


    private async Task<bool> Insertar(Periodista periodista)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        periodista.FechaRegistro = DateTime.Now;
     
    
        contexto.Periodistas.Add(periodista);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Periodista periodista)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Periodistas.Update(periodista);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Periodistas.AnyAsync(a => a.PeriodistaId == id);
    }
}


