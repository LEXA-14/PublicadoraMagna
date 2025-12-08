using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;


public class ServicioPromocionalService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
   
    public async Task<bool> Guardar(ServicioPromocional servicio)
    {

        if (!await Existe(servicio.ServicioPromocionalId))
        {
            return await Insertar(servicio);
        }
        else
        {
            return await Modificar(servicio);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var entidad = await contexto.ServiciosPromocionales.FindAsync(id);

        if (entidad == null) return false;

        contexto.ServiciosPromocionales.Remove(entidad);
        return await contexto.SaveChangesAsync() > 0;
    }

  
    public async Task<ServicioPromocional?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.ServiciosPromocionales.FirstOrDefaultAsync(s => s.ServicioPromocionalId == id);
    }

    public async Task<List<ServicioPromocional>> Listar()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.ServiciosPromocionales.ToListAsync();
    }

    private async Task<bool> Insertar(ServicioPromocional servicio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        servicio.FechaCreacion = DateTime.Now;
        contexto.ServiciosPromocionales.Add(servicio);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(ServicioPromocional servicio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.ServiciosPromocionales.Update(servicio);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.ServiciosPromocionales.AnyAsync(a => a.ServicioPromocionalId == id);
    }
}

