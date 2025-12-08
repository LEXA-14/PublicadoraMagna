using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;
using System.Linq.Expressions;

namespace PublicadoraMagna.Services;

public class ArticuloService(IDbContextFactory<ApplicationDbContext> dbFactory, PagoService pagoService)
{
    public async Task<bool> Guardar(Articulo articulo)
    {

        if (!await Existe(articulo.ArticuloId))
        {
            return await Insertar(articulo);
        }
        else
        {
            return await Modificar(articulo);
        }
    }

    public async Task<Articulo?> CambiarEstado(int id, EstadoArticulo nuevoEstado, string? comentario = null)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();


        var articulo = await contexto.Articulos.FindAsync(id);

        if (articulo == null) return null;

        articulo.Estado = nuevoEstado;

        if (nuevoEstado == EstadoArticulo.AprobadoInstitucion)
            articulo.FechaAprobacion = DateTime.Now;

        if (nuevoEstado == EstadoArticulo.AprobadoEditor)
            articulo.FechaAprobacion= DateTime.Now;
  
        if (nuevoEstado == EstadoArticulo.Enviado)
        {
            articulo.FechaPublicacion=DateTime.Now;
        }
        contexto.Articulos.Update(articulo);
        await contexto.SaveChangesAsync();

        if (nuevoEstado == EstadoArticulo.AprobadoEditor)
        {
            await pagoService.ProcesarPagosPorAprobacion(id);
        }
     
        return await GetArticuloCompleto(id);
    }
    public async Task<Articulo?> AprobarPorEditor(int articuloId, string? comentario = null)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var articulo = await contexto.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Institucion)
            .Include(a => a.Periodista)
            .Include(a => a.ServiciosPromocionales)
                .ThenInclude(asp => asp.ServicioPromocional)
            .FirstOrDefaultAsync(a => a.ArticuloId == articuloId);

        if (articulo == null) return null;

        
        if (articulo.Estado == EstadoArticulo.Rechazado) return null;
        if (articulo.Estado == EstadoArticulo.AprobadoEditor) return null;

        
        articulo.Estado = EstadoArticulo.AprobadoEditor;
        articulo.FechaAprobacion = DateTime.Now;
        articulo.FechaPublicacion = DateTime.Now;

        var guardado=await Modificar(articulo);

        if (!guardado) return null;

        await pagoService.ProcesarPagosPorAprobacion(articuloId);

        return await GetArticuloCompleto(articuloId);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var articulo = await contexto.Articulos
            .Include(a => a.ServiciosPromocionales)
            .FirstOrDefaultAsync(a => a.ArticuloId == id);

        if (articulo == null) return false;

        try
        {
            if (articulo.ServiciosPromocionales.Any())
            {
                contexto.ArticuloServicioPromocional.RemoveRange(articulo.ServiciosPromocionales);
            }
            var detallesPagoInstitucion = await contexto.DetallesPagosInstitucion
                .Where(d => d.ArticuloId == id)
                .ToListAsync();
            if (detallesPagoInstitucion.Any())
            {
                contexto.DetallesPagosInstitucion.RemoveRange(detallesPagoInstitucion);
            }
            var detallesPagoPeriodista = await contexto.DetallesPagosPeriodistas
                .Where(d => d.ArticuloId == id)
                .ToListAsync();
            if (detallesPagoPeriodista.Any())
            {
                contexto.DetallesPagosPeriodistas.RemoveRange(detallesPagoPeriodista);
            }
            var encargos = await contexto.EncargoArticulos
                .Where(e => e.ArticuloId == id)
                .ToListAsync();
            foreach (var encargo in encargos)
            {
                encargo.ArticuloId = null;
            }
            contexto.Articulos.Remove(articulo);

            return await contexto.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            
            throw new Exception($"Error al eliminar artículo: {ex.Message}");
        }
    }

    public async Task<List<Articulo>> ListarPorEstado(EstadoArticulo estado)
    {
         using var contexto = await dbFactory.CreateDbContextAsync();

        return await contexto.Articulos.Include(a => a.Categoria)
            .Include(a => a.Institucion)
            .Include(a => a.Periodista)
            .Include(a=>a.ServiciosPromocionales)
            .ThenInclude(asp=>asp.ServicioPromocional)
            .Where(a => a.Estado == estado)
            .OrderByDescending(a => a.FechaEnvio)
            .ToListAsync();
            }

    public async Task<ArticuloServicioPromocionales> AgregarServicio(int articuloId, ArticuloServicioPromocionales servicio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var articulo = await contexto.Articulos.FindAsync(articuloId);
        if (articulo == null) return null;


        if (servicio.PrecioAplicado < 0) return null;
          

        servicio.ArticuloId = articuloId;
        servicio.FechaAplicacion = DateTime.Now;
       
        contexto.ArticuloServicioPromocional.Add(servicio);
        await contexto.SaveChangesAsync();

        return servicio;
    }

    public async Task<bool> EliminarServicio(int servicioId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var servicio = await contexto.ArticuloServicioPromocional.FindAsync(servicioId);
        if (servicio == null) return false;

        contexto.ArticuloServicioPromocional.Remove(servicio);
        await contexto.SaveChangesAsync();

        return true;
    }

  

    private async Task<bool> Insertar(Articulo articulo)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        articulo.FechaCreacion = DateTime.Now;
        contexto.Articulos.Add(articulo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Articulo articulo)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Articulos.Update(articulo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Articulos.AnyAsync(a => a.ArticuloId == id);
    }

    
    public async Task<Articulo?> GetArticuloCompleto(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Institucion)
            .Include(a => a.Periodista)
            .Include(a => a.ServiciosPromocionales)
                .ThenInclude(asp => asp.ServicioPromocional)
            .FirstOrDefaultAsync(a => a.ArticuloId == id);
    }

    public async Task<List<Articulo>> GetLista(Expression<Func<Articulo, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Institucion)
            .Include(a => a.Periodista)
            .Include(a => a.ServiciosPromocionales)
                .ThenInclude(asp => asp.ServicioPromocional)
            .Where(criterio)
            .OrderByDescending(a => a.FechaCreacion)
            .ToListAsync();
    }


}


