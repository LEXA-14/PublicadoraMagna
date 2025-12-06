using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;
using System.Linq.Expressions;

namespace PublicadoraMagna.Services;

public class EncargoArticuloService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    

    public async Task<bool> Guardar(EncargoArticulo encargo)
    {
        if (!await Existe(encargo.EncargoArticuloId))
        {
            return await Insertar(encargo);
        }
        else
        {
            return await Modificar(encargo);
        }
    }

    private async Task<bool> Insertar(EncargoArticulo encargo)
    {
      

        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.EncargoArticulos.Add(encargo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(EncargoArticulo encargo)
    {
        if (string.IsNullOrWhiteSpace(encargo.TituloSugerido) || encargo.CategoriaId <= 0 || encargo.PeriodistaId <= 0) return false;

        await using var contexto = await dbFactory.CreateDbContextAsync();

        var encargoOriginal = await contexto.EncargoArticulos
            .Include(e => e.ServiciosPromocionales)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.EncargoArticuloId == encargo.EncargoArticuloId);

        if (encargoOriginal == null) return false;

        var detallesAnteriores = await contexto.EncargoServicioPromocional
            .Where(d => d.EncargoArticuloId == encargo.EncargoArticuloId)
            .ToListAsync();

        contexto.EncargoServicioPromocional.RemoveRange(detallesAnteriores);

        LimpiarServiciosPromocionales(encargo);

        contexto.EncargoArticulos.Update(encargo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var encargo = await contexto.EncargoArticulos
             .Include(d => d.ServiciosPromocionales)
             .FirstOrDefaultAsync(e => e.EncargoArticuloId == id);

        if (encargo == null) return false;

        contexto.EncargoServicioPromocional.RemoveRange(encargo.ServiciosPromocionales);
        contexto.EncargoArticulos.Remove(encargo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> ResponderEncargo(int encargoId, bool acepta, string? comentario = null)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var encargo = await contexto.EncargoArticulos.FindAsync(encargoId);
        if (encargo == null) return false;

        encargo.FechaRespuestaPeriodista = DateTime.Now;
        encargo.ComentarioPeriodista = comentario;
        encargo.Estado = acepta
            ? EstadoArticulo.Pendiente
            : EstadoArticulo.Rechazado;

        contexto.EncargoArticulos.Update(encargo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> EnviarArticuloAInstitucion(int encargoId, int articuloId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var encargo = await contexto.EncargoArticulos.FindAsync(encargoId);
        if (encargo == null) return false;

        encargo.ArticuloId = articuloId;
        encargo.FechaEnvioInstitucion = DateTime.Now;
        encargo.Estado = EstadoArticulo.Enviado;

        contexto.EncargoArticulos.Update(encargo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> ResponderArticuloInstitucion(int encargoId, bool aprueba, string? comentario = null)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var encargo = await contexto.EncargoArticulos
            .Include(e => e.Articulo)
            .FirstOrDefaultAsync(e => e.EncargoArticuloId == encargoId);

        if (encargo == null) return false;

        encargo.FechaRespuestaInstitucion = DateTime.Now;
        encargo.ComentarioInstitucion = comentario;

        if (aprueba)
        {
            encargo.Estado = EstadoArticulo.AprobadoInstitucion;

            if (encargo.ArticuloId.HasValue && encargo.Articulo != null)
            {
                encargo.Articulo.Estado = EstadoArticulo.AprobadoInstitucion;
                encargo.Articulo.FechaAprobacion = DateTime.Now;
            }
        }
        else
        {
            encargo.Estado = EstadoArticulo.Rechazado;
            encargo.ComentarioRechazo = comentario;

           
            if (encargo.ArticuloId.HasValue && encargo.Articulo != null)
            {
                encargo.Articulo.Estado = EstadoArticulo.Rechazado;
            }
        }

        contexto.EncargoArticulos.Update(encargo);
        return await contexto.SaveChangesAsync() > 0;
    }


    public async Task<bool> Cancelar(int encargoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var encargo = await contexto.EncargoArticulos.FindAsync(encargoId);
        if (encargo == null) return false;

        encargo.Estado = EstadoArticulo.Rechazado;
        contexto.EncargoArticulos.Update(encargo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<EncargoArticulo?> BuscarId(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        return await contexto.EncargoArticulos
            .Include(e => e.Institucion)
            .Include(e => e.Periodista)
            .Include(e => e.Categoria)
            .Include(e => e.ServiciosPromocionales)
                .ThenInclude(sa => sa.ServicioPromocional)
            .Include(e => e.Articulo)
            .FirstOrDefaultAsync(p => p.EncargoArticuloId == id);
    }

    public async Task<List<EncargoArticulo>> GetLista(Expression<Func<EncargoArticulo, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        return await contexto.EncargoArticulos
            .Include(e=>e.Articulo)
            .Include(e => e.Institucion)
            .Include(e => e.Periodista)
            .Include(e => e.Categoria)
            .Include(e => e.ServiciosPromocionales)
                .ThenInclude(sa => sa.ServicioPromocional)
            .Where(criterio)
            .OrderByDescending(e => e.FechaCreacion)
            .ToListAsync();
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.EncargoArticulos.AnyAsync(a => a.EncargoArticuloId == id);
    }

    private void LimpiarServiciosPromocionales(EncargoArticulo encargo)
    {
        foreach (var asociacion in encargo.ServiciosPromocionales)
        {
            asociacion.ServicioPromocional = null;
        }
    }
}

