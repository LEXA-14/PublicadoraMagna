using Microsoft.AspNetCore.Identity;
using PublicadoraMagna.Data;
using PublicadoraMagna.Model;

namespace PublicadoraMagna.Services;

public class DataSeederService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public DataSeederService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        // 1. Crear roles si no existen
        await CrearRoles();

        // 2. Crear usuario Admin si no existe
        await CrearUsuarioAdmin();

        // 3. Crear institución de prueba con admin
        await CrearInstitucionPrueba();

        // 4. Crear periodista de prueba
        await CrearPeriodistaPrueba();

        // 5. Crear categorías de prueba
        await CrearCategorias();

        // 6. Crear servicios promocionales de prueba
        await CrearServiciosPromocionales();

 
    }

    private async Task CrearRoles()
    {
        string[] roleNames = 
        { 
            AppRoles.Admin, 
            AppRoles.Editor, 
            AppRoles.AdminInstitucion, 
            AppRoles.RedactorInstitucion, 
            AppRoles.Periodista 
        };

        foreach (var roleName in roleNames)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"✅ Rol creado: {roleName}");
            }
        }
    }

    private async Task CrearUsuarioAdmin()
    {
        var adminEmail = "admin@publicadora.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                NombreCompleto = "Administrador Principal",
                EmailConfirmed = true,
                //FechaRegistro = DateTime.Now
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
                Console.WriteLine($"✅ Usuario Admin creado: {adminEmail} / Admin123!");
            }
        }
    }

    private async Task CrearInstitucionPrueba()
    {
        var institucionEmail = "admin@pucmm.edu.do";
        var institucionUser = await _userManager.FindByEmailAsync(institucionEmail);

        if (institucionUser == null)
        {
            // Crear institución
            var institucion = new Institucion
            {
                Nombre = "PUCMM",
                Rnc = "401-50002-3",
                Telefono = "809-580-1962",
                EmailAdmin = institucionEmail,
                CorreoContacto = institucionEmail,
                FechaRegistro = DateTime.Now
            };

            _context.Instituciones.Add(institucion);
            await _context.SaveChangesAsync();

            // Crear usuario admin de la institución
            institucionUser = new ApplicationUser
            {
                UserName = institucionEmail,
                Email = institucionEmail,
                NombreCompleto = "Admin PUCMM",
                InstitucionId = institucion.InstitucionId,
                EmailConfirmed = true,
                //FechaRegistro = DateTime.Now
            };

            var result = await _userManager.CreateAsync(institucionUser, "Pucmm123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(institucionUser, AppRoles.AdminInstitucion);
                Console.WriteLine($"✅ Institución y Admin creados: {institucionEmail} / Pucmm123!");
            }
        }
    }

    private async Task CrearPeriodistaPrueba()
    {
        var periodistaEmail = "juan.perez@periodista.com";
        var periodistaUser = await _userManager.FindByEmailAsync(periodistaEmail);

        if (periodistaUser == null)
        {
            // Crear periodista
            var periodista = new Periodista
            {
                Nombres = "Juan Pérez",
                EsActivo = true,
                TarifaBase = 7000,
                FechaRegistro = DateTime.Now
            };

            _context.Periodistas.Add(periodista);
            await _context.SaveChangesAsync();

            // Crear usuario periodista
            periodistaUser = new ApplicationUser
            {
                UserName = periodistaEmail,
                Email = periodistaEmail,
                NombreCompleto = "Juan Pérez",
                PeriodistaId = periodista.PeriodistaId,
                EmailConfirmed = true,
                //FechaRegistro = DateTime.Now
            };

            var result = await _userManager.CreateAsync(periodistaUser, "Periodista123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(periodistaUser, AppRoles.Periodista);
                Console.WriteLine($"✅ Periodista creado: {periodistaEmail} / Periodista123!");
            }
        }
    }

    private async Task CrearCategorias()
    {
        if (!_context.Categorias.Any())
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Nombre = "Tecnología", PrecioBase = 3000, FechaCreacion = DateTime.Now },
                new Categoria { Nombre = "Deportes", PrecioBase = 2500, FechaCreacion = DateTime.Now },
                new Categoria { Nombre = "Política", PrecioBase = 4000, FechaCreacion = DateTime.Now },
                new Categoria { Nombre = "Cultura", PrecioBase = 2000, FechaCreacion = DateTime.Now },
                new Categoria { Nombre = "Economía", PrecioBase = 3500, FechaCreacion = DateTime.Now }
            };

            _context.Categorias.AddRange(categorias);
            await _context.SaveChangesAsync();
            Console.WriteLine($"✅ {categorias.Count} categorías creadas");
        }
    }
   
    private async Task CrearServiciosPromocionales()
    {
        if (!_context.ServiciosPromocionales.Any())
        {
            var servicios = new List<ServicioPromocional>
            {
                new ServicioPromocional 
                { 
                    Nombre = "Portada destacada", 
                    Descripcion = "Aparece en la portada principal por 24 horas",
                    Precio = 1500, 
                    FechaCreacion = DateTime.Now 
                },
                new ServicioPromocional 
                { 
                    Nombre = "Redes sociales", 
                    Descripcion = "Publicación en todas las redes sociales",
                    Precio = 800, 
                    FechaCreacion = DateTime.Now 
                },
                new ServicioPromocional 
                { 
                    Nombre = "Banner principal", 
                    Descripcion = "Banner en la página principal por 7 días",
                    Precio = 2000, 
                    FechaCreacion = DateTime.Now 
                },
                new ServicioPromocional 
                { 
                    Nombre = "Newsletter", 
                    Descripcion = "Inclusión en el newsletter semanal",
                    Precio = 1000, 
                    FechaCreacion = DateTime.Now 
                }
            };

            _context.ServiciosPromocionales.AddRange(servicios);
            await _context.SaveChangesAsync();
            Console.WriteLine($"✅ {servicios.Count} servicios promocionales creados");
        }
    }
}
