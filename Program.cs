using Tp_inmobiliaria.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tp_inmobiliaria.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registro del servicio de conexión y repositorios
builder.Services.AddSingleton<ConexionBD>();
builder.Services.AddScoped<RepositorioUsuario>();

// Configuración de autenticación por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";   // redirige si no está logueado
        options.LogoutPath = "/Auth/Logout"; // ruta para cerrar sesión
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // expira en 30 minutos
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  

app.UseRouting();


app.UseAuthentication(); // primero autenticación
app.UseAuthorization();  // después autorización

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

