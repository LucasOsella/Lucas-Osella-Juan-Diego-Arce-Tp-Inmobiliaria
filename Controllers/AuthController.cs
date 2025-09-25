using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers
{
    public class AuthController : Controller
    {
        private readonly RepositorioUsuario repo;

        public AuthController(RepositorioUsuario repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // 1. Validar campos vacíos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Debe ingresar usuario y contraseña";
                return View();
            }

            // 2. Buscar usuario en la DB
            var usuario = repo.Login(email); // tu método de acceso
            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            // 3. Verificar contraseña con hash
            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, usuario.Password, password);
            if (resultado == PasswordVerificationResult.Failed)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View();
            }

            // 4. Crear los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario + " " + usuario.ApellidoUsuario),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.RolUsuario.ToString()), // Asumiendo que RolUsuario es un entero
                new Claim("UserId", usuario.Id.ToString()),
                new Claim("foto", usuario.foto.ToString() ?? "/images/usuarios/default.png")
            };

            // 5. Generar la identidad
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // 6. Guardar cookie de sesión
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true, // Mantiene la sesión
                    ExpiresUtc = DateTime.UtcNow.AddHours(1) // Expira en 1 hora
                });
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ListaUsuario()
        {
            var lista = repo.ObtenerUsuarios();
            return View(lista); // Busca Views/Auth/ListaUsuario.cshtml
        }
        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario usuario, IFormFile FotoArchivo)
        { 
                // Hashear la contraseña antes de guardarla
                var hasher = new PasswordHasher<Usuario>();
                usuario.Password = hasher.HashPassword(usuario, usuario.Password);
                usuario.Activo = 1; // Asegurarse de que el usuario esté activo al crearlo
                usuario.foto = "/images/usuarios/default.png"; // Ruta por defecto si no se sube ninguna foto
                repo.CrearUsuario(usuario);
                return RedirectToAction("Index", "Home");
        }


        public IActionResult EditarUsuario(int id)
        {
            var usuario = repo.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);// busca
        }

        public async Task <IActionResult> GuardarEdicion(Usuario usuario, IFormFile FotoArchivo)
        {
            if (FotoArchivo != null && FotoArchivo.Length > 0)
            {
                //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(FotoArchivo.FileName);
                var fileName = usuario.Id + Path.GetExtension(FotoArchivo.FileName); // Usar el ID del usuario como nombre de archivo
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/usuarios", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FotoArchivo.CopyToAsync(stream);
                }
                usuario.foto = "/images/usuarios/" + fileName; // se guarda la ruta en la BD
            }
            else
            {
                //var usuariofoto = repo.ObtenerUsuarioPorId(usuario.Id);
                //if (usuariofoto != null)
                //{
                //    usuario.foto = usuariofoto.foto;
                //}
                usuario.foto = usuario.foto; // Mantener la foto existente
            }
            var usuarioExistente = repo.ObtenerUsuarioPorId(usuario.Id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }
            // Hashear la contraseña antes de guardarla
            if (!string.IsNullOrEmpty(usuario.Password))
            {
                var hasher = new PasswordHasher<Usuario>();
                usuarioExistente.Password = hasher.HashPassword(usuarioExistente, usuario.Password);
            } 
            usuarioExistente.NombreUsuario = usuario.NombreUsuario;
            usuarioExistente.ApellidoUsuario = usuario.ApellidoUsuario; 
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.IdTipoUsuario = usuario.IdTipoUsuario;
            usuarioExistente.foto = usuario.foto;           
            repo.EditarUsuario(usuarioExistente);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult EliminarUsuario(int id)
        {
            var usuario = repo.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            repo.EliminarUsuario(id);
            return RedirectToAction("ListaUsuario");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }

//hola    
}
