using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class InstitucionService(IDbContextFactory<ApplicationDbContext> dbFactory, UserManager<ApplicationUser> userManager)
{

    public async Task<bool> Guardar(Institucion institucion)
    {
        if (!await Existe(institucion.InstitucionId))
        {
            return await Insertar(institucion);
        }
        else
        {
            return await Modificar(institucion);
        }
    }

    public async Task<bool> CrearConAdministrador(Institucion institucion,string nombreAdmin,string emailAdmin,string passwordAdmin)
    {
       
        var usuarioExiste = await userManager.FindByEmailAsync(emailAdmin);
        if (usuarioExiste != null)
            throw new Exception("Ya existe un usuario con este email");

      
        institucion.EmailAdmin = emailAdmin;
        institucion.CorreoContacto = emailAdmin;
        institucion.FechaRegistro = DateTime.Now;

        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Instituciones.Add(institucion);
        await contexto.SaveChangesAsync();

      
        var adminUser = new ApplicationUser
        {
            UserName = emailAdmin,
            Email = emailAdmin,
            NombreCompleto = nombreAdmin,
            InstitucionId = institucion.InstitucionId,
            EmailConfirmed = true,
            //FechaRegistro = DateTime.Now
        };

        var result = await userManager.CreateAsync(adminUser, passwordAdmin);
        if (!result.Succeeded)
        {
            var errores = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Error al crear usuario: {errores}");
        }
        await userManager.AddToRoleAsync(adminUser, "AdminInstitucion");

        return true;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();

        var entidad = await contexto.Instituciones
            .Include(i => i.Articulos)
            .Include(i => i.Usuarios)
            .FirstOrDefaultAsync(i => i.InstitucionId == id);

        if (entidad == null) return false;
        contexto.Instituciones.Remove(entidad);
        var result = await contexto.SaveChangesAsync();
        return result > 0;
    }



    public async Task<Institucion?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Instituciones
             .Include(i => i.Usuarios)
            .Include(i => i.Articulos)
            .FirstOrDefaultAsync(i => i.InstitucionId == id);
    }

    public async Task<List<Institucion>> Listar()
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Instituciones
            .Include(i => i.Usuarios)
            .Include(i => i.Articulos).ToListAsync();
    }


    private async Task<bool> Insertar(Institucion institucion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Instituciones.Add(institucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Institucion institucion)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Instituciones.Update(institucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Instituciones.AnyAsync(a => a.InstitucionId == id);
    }
}
