using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class PagoService(IDbContextFactory<ApplicationDbContext> dbFactory)
{
    public async Task<bool> ProcesarPagosPorAprobacion(int articuloId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var articulo = await contexto.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Institucion)
            .Include(a => a.Periodista)
            .Include(a => a.ServiciosPromocionales)
                .ThenInclude(asp => asp.ServicioPromocional)
            .FirstOrDefaultAsync(a => a.ArticuloId == articuloId);

        if (articulo == null) return false;

        await GenerarPagoInstitucion(articulo, contexto);
        await GenerarPagoPeriodista(articulo, contexto);

        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task GenerarPagoInstitucion(Articulo articulo, ApplicationDbContext contexto)
    {
       
        if (!articulo.InstitucionId.HasValue) return;

        var detalleExistente = await contexto.DetallesPagosInstitucion
      .AnyAsync(d => d.ArticuloId == articulo.ArticuloId);

        if (detalleExistente)
        {
            return;
        }


        decimal precioBase = articulo.Categoria?.PrecioBase ?? 0m;
        decimal totalServicios = articulo.ServiciosPromocionales?.Sum(s => s.PrecioAplicado) ?? 0m;
        decimal tarifaPeriodista = articulo.Periodista?.TarifaBase ?? 0m;

       
        decimal montoTotal = precioBase + totalServicios + tarifaPeriodista;

        if (montoTotal <= 0) return;

     
        var pagoMaestro = await contexto.PagosInstitucion
            .Include(p => p.DetallesPagos)
            .FirstOrDefaultAsync(p =>
                p.InstitucionId == articulo.InstitucionId.Value &&
                p.Estado == "Pendiente" &&
                p.FechaPago.Date == DateTime.Now.Date);

        if (pagoMaestro == null)
        {
            pagoMaestro = new PagoInstitucion
            {
                InstitucionId = articulo.InstitucionId.Value,
                FechaPago = DateTime.Now,
                Monto = 0,
                Estado = "Pendiente",
                DetallesPagos = new List<DetallePagoInstitucion>()
            };
            contexto.PagosInstitucion.Add(pagoMaestro);
            await contexto.SaveChangesAsync(); 
        }

     
        var detalle = new DetallePagoInstitucion
        {
            ArticuloId = articulo.ArticuloId,
            Monto = montoTotal,
            PagoInstitucionId = pagoMaestro.PagoId
        };

        pagoMaestro.DetallesPagos.Add(detalle);
        pagoMaestro.Monto += montoTotal;
    }

    private async Task GenerarPagoPeriodista(Articulo articulo, ApplicationDbContext contexto)
    {
        
        if (!articulo.PeriodistaId.HasValue) return;
        var detalleExistente = await contexto.DetallesPagosPeriodistas
       .AnyAsync(d => d.ArticuloId == articulo.ArticuloId);

        if (detalleExistente)
        {
            return;
        }

        decimal tarifaPeriodista = articulo.Periodista?.TarifaBase ?? 0m;

        if (tarifaPeriodista <= 0) return;

        int periodistaId = articulo.PeriodistaId.Value;

        
        var pagoMaestro = await contexto.PagosPeriodistas
            .Include(p => p.Detalles)
            .FirstOrDefaultAsync(p =>
                p.PeriodistaId == periodistaId &&
                p.Estado == "Pendiente" &&  
                p.Fecha.Date == DateTime.Now.Date);

        if (pagoMaestro == null)
        {
            pagoMaestro = new PagoPeriodista
            {
                PeriodistaId = periodistaId,
                Fecha = DateTime.Now,
                Total = 0,
                Estado = "Pendiente", 
                Detalles = new List<DetallePagoPeriodista>()
            };
            contexto.PagosPeriodistas.Add(pagoMaestro);
            await contexto.SaveChangesAsync(); 
        }

        
        var detalle = new DetallePagoPeriodista
        {
            ArticuloId = articulo.ArticuloId,
            Monto = tarifaPeriodista,
            PagoPeriodistaId = pagoMaestro.PagoPeriodistaId
        };

        pagoMaestro.Detalles.Add(detalle);
        pagoMaestro.Total += tarifaPeriodista;
    }

    public async Task<bool> MarcarPagoInstitucionComoPagado(int articuloId, string numeroTransaccion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        
        var detallePago = await contexto.DetallesPagosInstitucion
            .Include(d => d.Pago)
            .FirstOrDefaultAsync(d => d.ArticuloId == articuloId && d.Pago.Estado == "Pendiente");
       
        if (detallePago == null) return false;

        var pagoMaestro = detallePago.Pago;

        if (pagoMaestro == null) return false;
        pagoMaestro.Estado = "Pagado";
        pagoMaestro.FechaPago = DateTime.Now;

        contexto.PagosInstitucion.Update(pagoMaestro);
        var articulo = await contexto.Articulos
      .FirstOrDefaultAsync(a => a.ArticuloId == articuloId);

        if (articulo != null)
        {
            Console.WriteLine($"Actualizando artículo {articulo.ArticuloId} a estado Pagado");
            articulo.Estado = EstadoArticulo.Pagado;
            contexto.Articulos.Update(articulo);
        }
        return await contexto.SaveChangesAsync() > 0;
    }


}