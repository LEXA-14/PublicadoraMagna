//using Microsoft.AspNetCore.Identity;
//using PublicadoraMagna.Data;
//using PublicadoraMagna.Model;

//namespace PublicadoraMagna.Services;

//public class DataSeederService
//{
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly RoleManager<IdentityRole> _roleManager;
//    private readonly ApplicationDbContext _context;

//    public DataSeederService(
//        UserManager<ApplicationUser> userManager,
//        RoleManager<IdentityRole> roleManager,
//        ApplicationDbContext context)
//    {
//        _userManager = userManager;
//        _roleManager = roleManager;
//        _context = context;
//    }

//    public async Task SeedDataAsync()
//    {
//        // 1. Crear roles si no existen
//        await CrearRoles();

//        // 2. Crear usuario Admin si no existe
//        await CrearUsuarioAdmin();

//        // 3. Crear institución de prueba con admin
//        await CrearInstitucionPrueba();

//        // 4. Crear periodista de prueba
//        await CrearPeriodistaPrueba();

//        // 5. Crear categorías de prueba
//        await CrearCategorias();

//        // 6. Crear servicios promocionales de prueba
//        await CrearServiciosPromocionales();

//        await CrearEditorPrueba();



//    }

//    private async Task CrearRoles()
//    {
//        string[] roleNames = 
//        { 
//            AppRoles.Admin, 
//            AppRoles.Editor, 
//            AppRoles.AdminInstitucion, 
//            AppRoles.RedactorInstitucion, 
//            AppRoles.Periodista 
//        };

//        foreach (var roleName in roleNames)
//        {
//            if (!await _roleManager.RoleExistsAsync(roleName))
//            {
//                await _roleManager.CreateAsync(new IdentityRole(roleName));
//                Console.WriteLine($"✅ Rol creado: {roleName}");
//            }
//        }
//    }

//    private async Task CrearUsuarioAdmin()
//    {
//        var adminEmail = "admin@publicadora.com";
//        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

//        if (adminUser == null)
//        {
//            adminUser = new ApplicationUser
//            {
//                UserName = adminEmail,
//                Email = adminEmail,
//                NombreCompleto = "Administrador Principal",
//                EmailConfirmed = true,
//                //FechaRegistro = DateTime.Now
//            };

//            var result = await _userManager.CreateAsync(adminUser, "Admin123!");

//            if (result.Succeeded)
//            {
//                await _userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
//                Console.WriteLine($"✅ Usuario Admin creado: {adminEmail} / Admin123!");
//            }
//        }
//    }

//    private async Task CrearInstitucionPrueba()
//    {
//        var institucionEmail = "admin@pucmm.edu.do";
//        var institucionUser = await _userManager.FindByEmailAsync(institucionEmail);

//        if (institucionUser == null)
//        {
//            // Crear institución
//            var institucion = new Institucion
//            {
//                Nombre = "PUCMM",
//                Rnc = "401-50002-3",
//                Telefono = "809-580-1962",
//                EmailAdmin = institucionEmail,
//                CorreoContacto = institucionEmail,
//                FechaRegistro = DateTime.Now
//            };

//            _context.Instituciones.Add(institucion);
//            await _context.SaveChangesAsync();

//            // Crear usuario admin de la institución
//            institucionUser = new ApplicationUser
//            {
//                UserName = institucionEmail,
//                Email = institucionEmail,
//                NombreCompleto = "Admin PUCMM",
//                InstitucionId = institucion.InstitucionId,
//                EmailConfirmed = true,
//                //FechaRegistro = DateTime.Now
//            };

//            var result = await _userManager.CreateAsync(institucionUser, "Pucmm123!");

//            if (result.Succeeded)
//            {
//                await _userManager.AddToRoleAsync(institucionUser, AppRoles.AdminInstitucion);
//                Console.WriteLine($"✅ Institución y Admin creados: {institucionEmail} / Pucmm123!");
//            }
//        }
//    }

//    private async Task CrearPeriodistaPrueba()
//    {
//        var periodistaEmail = "juan.perez@periodista.com";
//        var periodistaUser = await _userManager.FindByEmailAsync(periodistaEmail);

//        if (periodistaUser == null)
//        {
//            // Crear periodista
//            var periodista = new Periodista
//            {
//                Nombres = "Juan Pérez",
//                EsActivo = true,
//                TarifaBase = 7000,
//                FechaRegistro = DateTime.Now
//            };

//            _context.Periodistas.Add(periodista);
//            await _context.SaveChangesAsync();

//            // Crear usuario periodista
//            periodistaUser = new ApplicationUser
//            {
//                UserName = periodistaEmail,
//                Email = periodistaEmail,
//                NombreCompleto = "Juan Pérez",
//                PeriodistaId = periodista.PeriodistaId,
//                EmailConfirmed = true,
//                //FechaRegistro = DateTime.Now
//            };

//            var result = await _userManager.CreateAsync(periodistaUser, "Periodista123!");

//            if (result.Succeeded)
//            {
//                await _userManager.AddToRoleAsync(periodistaUser, AppRoles.Periodista);
//                Console.WriteLine($"✅ Periodista creado: {periodistaEmail} / Periodista123!");
//            }
//        }
//    }



//    private async Task CrearCategorias()
//    {
//        if (!_context.Categorias.Any())
//        {
//            var categorias = new List<Categoria>
//            {
//                new Categoria { Nombre = "Tecnología", PrecioBase = 3000, FechaCreacion = DateTime.Now },
//                new Categoria { Nombre = "Deportes", PrecioBase = 2500, FechaCreacion = DateTime.Now },
//                new Categoria { Nombre = "Política", PrecioBase = 4000, FechaCreacion = DateTime.Now },
//                new Categoria { Nombre = "Cultura", PrecioBase = 2000, FechaCreacion = DateTime.Now },
//                new Categoria { Nombre = "Economía", PrecioBase = 3500, FechaCreacion = DateTime.Now }
//            };

//            _context.Categorias.AddRange(categorias);
//            await _context.SaveChangesAsync();
//            Console.WriteLine($"✅ {categorias.Count} categorías creadas");
//        }
//    }
//    private async Task CrearEditorPrueba()
//    {
//        var editorEmail = "editor@publicadora.com";
//        var editorUser = await _userManager.FindByEmailAsync(editorEmail);

//        if (editorUser == null)
//        {
//            editorUser = new ApplicationUser
//            {
//                UserName = editorEmail,
//                Email = editorEmail,
//                NombreCompleto = "Editor Prueba",
//                EmailConfirmed = true,
//                //FechaRegistro = DateTime.Now
//            };

//            var result = await _userManager.CreateAsync(editorUser, "Prueba123@");

//            if (result.Succeeded)
//            {
//                await _userManager.AddToRoleAsync(editorUser, AppRoles.Editor);
//                Console.WriteLine($"✅ Editor creado: {editorEmail} / Prueba123@");
//            }
//        }
//    }


//    private async Task CrearServiciosPromocionales()
//    {
//        if (!_context.ServiciosPromocionales.Any())
//        {
//            var servicios = new List<ServicioPromocional>
//            {
//                new ServicioPromocional 
//                { 
//                    Nombre = "Portada destacada", 
//                    Descripcion = "Aparece en la portada principal por 24 horas",
//                    Precio = 1500, 
//                    FechaCreacion = DateTime.Now 
//                },
//                new ServicioPromocional 
//                { 
//                    Nombre = "Redes sociales", 
//                    Descripcion = "Publicación en todas las redes sociales",
//                    Precio = 800, 
//                    FechaCreacion = DateTime.Now 
//                },
//                new ServicioPromocional 
//                { 
//                    Nombre = "Banner principal", 
//                    Descripcion = "Banner en la página principal por 7 días",
//                    Precio = 2000, 
//                    FechaCreacion = DateTime.Now 
//                },
//                new ServicioPromocional 
//                { 
//                    Nombre = "Newsletter", 
//                    Descripcion = "Inclusión en el newsletter semanal",
//                    Precio = 1000, 
//                    FechaCreacion = DateTime.Now 
//                }
//            };

//            _context.ServiciosPromocionales.AddRange(servicios);
//            await _context.SaveChangesAsync();
//            Console.WriteLine($"✅ {servicios.Count} servicios promocionales creados");
//        }
//    }
//}

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

        // 3. Crear Editor
        await CrearEditorPrueba();

        // 4. Crear 5 categorías
        await CrearCategorias();

        // 5. Crear 5 servicios promocionales
        await CrearServiciosPromocionales();

        // 6. Crear 5 instituciones con sus admins
        await CrearInstituciones();

        // 7. Crear 5 periodistas con sus usuarios
        await CrearPeriodistas();

        // 8. Crear algunos artículos y encargos de ejemplo
        await CrearArticulosYEncargos();

        Console.WriteLine("✅ ✅ ✅ SEED COMPLETADO ✅ ✅ ✅");
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
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
                Console.WriteLine($"✅ Usuario Admin creado: {adminEmail} / Admin123!");
            }
        }
    }

    private async Task CrearEditorPrueba()
    {
        var editorEmail = "editor@publicadora.com";
        var editorUser = await _userManager.FindByEmailAsync(editorEmail);

        if (editorUser == null)
        {
            editorUser = new ApplicationUser
            {
                UserName = editorEmail,
                Email = editorEmail,
                NombreCompleto = "Editor Principal",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(editorUser, "Editor123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(editorUser, AppRoles.Editor);
                Console.WriteLine($"✅ Editor creado: {editorEmail} / Editor123!");
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
                    Nombre = "Portada Destacada",
                    Descripcion = "Aparece en la portada principal por 24 horas",
                    Precio = 1500,
                    FechaCreacion = DateTime.Now
                },
                new ServicioPromocional
                {
                    Nombre = "Redes Sociales",
                    Descripcion = "Publicación en todas las redes sociales",
                    Precio = 800,
                    FechaCreacion = DateTime.Now
                },
                new ServicioPromocional
                {
                    Nombre = "Banner Principal",
                    Descripcion = "Banner en la página principal por 7 días",
                    Precio = 2000,
                    FechaCreacion = DateTime.Now
                },
                new ServicioPromocional
                {
                    Nombre = "Newsletter Semanal",
                    Descripcion = "Inclusión en el newsletter semanal",
                    Precio = 1000,
                    FechaCreacion = DateTime.Now
                },
                new ServicioPromocional
                {
                    Nombre = "Video Promocional",
                    Descripcion = "Video corto promocional en YouTube y redes",
                    Precio = 2500,
                    FechaCreacion = DateTime.Now
                }
            };

            _context.ServiciosPromocionales.AddRange(servicios);
            await _context.SaveChangesAsync();
            Console.WriteLine($"✅ {servicios.Count} servicios promocionales creados");
        }
    }

    private async Task CrearInstituciones()
    {
        if (!_context.Instituciones.Any())
        {
            var instituciones = new List<(Institucion institucion, string email, string password)>
            {
                (new Institucion
                {
                    Nombre = "PUCMM",
                    Rnc = "401-50002-3",
                    Telefono = "809-580-1962",
                    EmailAdmin = "admin@pucmm.edu.do",
                    CorreoContacto = "contacto@pucmm.edu.do",
                    FechaRegistro = DateTime.Now
                }, "admin@pucmm.edu.do", "Pucmm123!"),

                (new Institucion
                {
                    Nombre = "UASD",
                    Rnc = "401-50001-5",
                    Telefono = "809-686-5555",
                    EmailAdmin = "admin@uasd.edu.do",
                    CorreoContacto = "contacto@uasd.edu.do",
                    FechaRegistro = DateTime.Now
                }, "admin@uasd.edu.do", "Uasd123!"),

                (new Institucion
                {
                    Nombre = "INTEC",
                    Rnc = "401-50003-1",
                    Telefono = "809-567-9271",
                    EmailAdmin = "admin@intec.edu.do",
                    CorreoContacto = "contacto@intec.edu.do",
                    FechaRegistro = DateTime.Now
                }, "admin@intec.edu.do", "Intec123!"),

                (new Institucion
                {
                    Nombre = "UNPHU",
                    Rnc = "401-50004-9",
                    Telefono = "809-562-6121",
                    EmailAdmin = "admin@unphu.edu.do",
                    CorreoContacto = "contacto@unphu.edu.do",
                    FechaRegistro = DateTime.Now
                }, "admin@unphu.edu.do", "Unphu123!"),

                (new Institucion
                {
                    Nombre = "UNIBE",
                    Rnc = "401-50005-7",
                    Telefono = "809-689-4111",
                    EmailAdmin = "admin@unibe.edu.do",
                    CorreoContacto = "contacto@unibe.edu.do",
                    FechaRegistro = DateTime.Now
                }, "admin@unibe.edu.do", "Unibe123!")
            };

            foreach (var (institucion, email, password) in instituciones)
            {
                _context.Instituciones.Add(institucion);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    NombreCompleto = $"Admin {institucion.Nombre}",
                    InstitucionId = institucion.InstitucionId,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AppRoles.AdminInstitucion);
                    Console.WriteLine($"✅ Institución y Admin creados: {email} / {password}");
                }
            }
        }
    }

    private async Task CrearPeriodistas()
    {
        if (!_context.Periodistas.Any())
        {
            var periodistas = new List<(Periodista periodista, string email, string password)>
            {
                (new Periodista
                {
                    Nombres = "Juan Pérez",
                    EsActivo = true,
                    TarifaBase = 5000,
                    FechaRegistro = DateTime.Now
                }, "juan.perez@periodista.com", "Juan123!"),

                (new Periodista
                {
                    Nombres = "María Rodríguez",
                    EsActivo = true,
                    TarifaBase = 6000,
                    FechaRegistro = DateTime.Now
                }, "maria.rodriguez@periodista.com", "Maria123!"),

                (new Periodista
                {
                    Nombres = "Carlos Martínez",
                    EsActivo = true,
                    TarifaBase = 5500,
                    FechaRegistro = DateTime.Now
                }, "carlos.martinez@periodista.com", "Carlos123!"),

                (new Periodista
                {
                    Nombres = "Ana García",
                    EsActivo = true,
                    TarifaBase = 7000,
                    FechaRegistro = DateTime.Now
                }, "ana.garcia@periodista.com", "Ana123!"),

                (new Periodista
                {
                    Nombres = "Luis Fernández",
                    EsActivo = false,
                    TarifaBase = 4500,
                    FechaRegistro = DateTime.Now.AddMonths(-6)
                }, "luis.fernandez@periodista.com", "Luis123!")
            };

            foreach (var (periodista, email, password) in periodistas)
            {
                _context.Periodistas.Add(periodista);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    NombreCompleto = periodista.Nombres,
                    PeriodistaId = periodista.PeriodistaId,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AppRoles.Periodista);
                    Console.WriteLine($"✅ Periodista creado: {email} / {password}");
                }
            }
        }
    }

    private async Task CrearArticulosYEncargos()
    {
        if (!_context.Articulos.Any() && !_context.EncargoArticulos.Any())
        {
            var categorias = _context.Categorias.ToList();
            var instituciones = _context.Instituciones.ToList();
            var periodistas = _context.Periodistas.Where(p => p.EsActivo).ToList();
            var servicios = _context.ServiciosPromocionales.ToList();

            // Crear 5 Encargos
            for (int i = 1; i <= 5; i++)
            {
                var encargo = new EncargoArticulo
                {
                    InstitucionId = instituciones[i % instituciones.Count].InstitucionId,
                    PeriodistaId = periodistas[i % periodistas.Count].PeriodistaId,
                    TituloSugerido = $"Encargo de Artículo {i}: Tema Importante",
                    DescripcionEncargo = $"Este es un encargo importante sobre un tema relevante para nuestra institución. Necesitamos que cubra los aspectos principales del tema {i}.",
                    CategoriaId = categorias[i % categorias.Count].CategoriaId,
                    Estado = i switch
                    {
                        1 => EstadoArticulo.Pendiente,
                        2 => EstadoArticulo.Enviado,
                        3 => EstadoArticulo.AprobadoInstitucion,
                        4 => EstadoArticulo.Rechazado,
                        _ => EstadoArticulo.Pendiente
                    },
                    FechaCreacion = DateTime.Now.AddDays(-i * 2),
                    ComentarioRechazo = i == 4 ? "El contenido no cumple con los estándares requeridos" : null
                };

                // Agregar 2 servicios promocionales al encargo
                if (i <= 2)
                {
                    encargo.ServiciosPromocionales.Add(new EncargoServicioPromocional
                    {
                        ServicioPromocionalId = servicios[0].ServicioPromocionalId,
                        PrecioAplicado = servicios[0].Precio,
                        FechaAplicacion = DateTime.Now
                    });
                }

                _context.EncargoArticulos.Add(encargo);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("✅ 5 encargos creados");

            // Crear 5 Artículos
            for (int i = 1; i <= 5; i++)
            {
                var articulo = new Articulo
                {
                    Titulo = $"Artículo {i}: Análisis Completo del Tema",
                    Resumen = $"Este es un resumen del artículo {i} que proporciona una vista general del contenido que será desarrollado en profundidad.",
                    Contenido = $@"Este es el contenido completo del artículo {i}. 

En este análisis exhaustivo, exploramos los diversos aspectos relacionados con el tema principal. La investigación realizada ha revelado datos importantes que merecen ser destacados.

Punto 1: Contexto histórico y relevancia actual
El tema ha cobrado especial importancia en los últimos años debido a múltiples factores sociales, económicos y políticos que han confluido para crear el escenario actual.

Punto 2: Análisis de la situación actual
Los expertos consultados coinciden en que la situación requiere atención inmediata y soluciones innovadoras que puedan ser implementadas de manera efectiva.

Punto 3: Perspectivas futuras
Mirando hacia adelante, es fundamental considerar las implicaciones a largo plazo de las decisiones que se tomen en el presente.

Conclusión: Este artículo proporciona una visión integral del tema, destacando los aspectos más relevantes y ofreciendo perspectivas para el futuro.",

                    CategoriaId = categorias[i % categorias.Count].CategoriaId,

                    // Alternar entre artículos de periodista e institución
                    PeriodistaId = i % 2 == 0 ? periodistas[i % periodistas.Count].PeriodistaId : (int?)null,
                    InstitucionId = i % 2 != 0 ? instituciones[i % instituciones.Count].InstitucionId : (int?)null,

                    EsLibre = i % 2 == 0,

                    Estado = i switch
                    {
                        1 => EstadoArticulo.Borrador,
                        2 => EstadoArticulo.Pendiente,
                        3 => EstadoArticulo.AprobadoInstitucion,
                        4 => EstadoArticulo.AprobadoEditor,
                        _ => EstadoArticulo.Pagado
                    },

                    FechaCreacion = DateTime.Now.AddDays(-i * 3),
                    FechaEnvio = i >= 2 ? DateTime.Now.AddDays(-i * 2) : null,
                    FechaAprobacion = i >= 4 ? DateTime.Now.AddDays(-i) : null,
                    FechaPublicacion = i == 5 ? DateTime.Now : null
                };

                // Agregar servicios promocionales a artículos de instituciones
                if (articulo.InstitucionId.HasValue && i <= 3)
                {
                    articulo.ServiciosPromocionales.Add(new ArticuloServicioPromocionales
                    {
                        ServicioPromocionalId = servicios[i % servicios.Count].ServicioPromocionalId,
                        PrecioAplicado = servicios[i % servicios.Count].Precio,
                        FechaAplicacion = DateTime.Now
                    });
                }

                _context.Articulos.Add(articulo);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("✅ 5 artículos creados");
        }
    }
}