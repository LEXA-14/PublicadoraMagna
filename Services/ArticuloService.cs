using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class ArticuloService(IDbContextFactory<ApplicationDbContext> dbFactory, PagoService pagoService)
{
    public async Task<bool> Guardar(Articulo articulo)
    {
       
        if (string.IsNullOrWhiteSpace(articulo.Titulo))
            throw new Exception("El título del artículo es requerido");
        if (string.IsNullOrWhiteSpace(articulo.Contenido))
            throw new Exception("El contenido del artículo es requerido");
        if (articulo.CategoriaId <= 0)
            throw new Exception("Debe seleccionar una categoría");

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
        var entidad = await contexto.Articulos.FindAsync(id);
        if (entidad == null) return false;

        contexto.Articulos.Remove(entidad);
        return await contexto.SaveChangesAsync() > 0;
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

 
}


