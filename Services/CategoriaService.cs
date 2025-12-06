using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class CategoriaService(IDbContextFactory<ApplicationDbContext> dbFactory)
{

 
    public async Task<bool> Guardar(Categoria categoria)
    {
        if (categoria.CategoriaId == 0)
        {
            categoria.FechaCreacion = DateTime.Now;
            return await Insertar(categoria);
        }
        else
        {
           return await Modificar(categoria);
        }
      
    }

    private async Task<bool>Insertar(Categoria categoria)
    {
        using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Add(categoria);
        return await contexto.SaveChangesAsync()>0;

    }

    public async Task<bool>Modificar(Categoria categoria)
    {
        using var contexto=await dbFactory.CreateDbContextAsync();
        contexto.Update(categoria);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool>Eliminar(int id)
    {
        using var contexto=await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias.AnyAsync(c=>c.CategoriaId==id);
    }
    public async Task<Categoria?> Buscar(int id)
    {
        using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias
            .FirstOrDefaultAsync(c => c.CategoriaId == id);
    }
    public async Task<List<Categoria>> Listar()
    {
        using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Categorias
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }
}
