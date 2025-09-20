using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Controllers;
public class TipoInmuebleController : Controller
{
    private readonly ILogger<TipoInmuebleController> _logger;
    RepositoriosTipoInmuebles repo;
    public TipoInmuebleController(ILogger<TipoInmuebleController> logger, ConexionBD conexionBD)
    {
        _logger = logger;
        repo = new RepositoriosTipoInmuebles(conexionBD);
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTipoInmuebles();
        return View(lista);// Busca Views/TipoInmueble/Index.cshtml
    }

    public IActionResult AgregarTipoInmueble()
    {
        return View(); // Busca Views/TipoInmueble/AgregarTipoInmueble.cshtml
    }

    public IActionResult GuardarTipo(Tipo_Inmueble Tipo_Inmueble)
    {
        repo.AgregarTipoInmueble(Tipo_Inmueble);
        return RedirectToAction("Index");
    }

    public IActionResult editarTipo(int id)
    {
        var Tipo_Inmueble = repo.ObtenerTipo(id);
        return View(Tipo_Inmueble);// Busca Views/TipoInmueble/editarTipo.cshtml
    }

    public IActionResult guardarTipoEditado(Tipo_Inmueble Tipo_Inmueble)
    {
        repo.editarTipo(Tipo_Inmueble);
        return RedirectToAction("Index");
    }
    
    public IActionResult eliminarTipo(int id)
    {
        repo.eliminarTipo(id);
        return RedirectToAction("Index");
    }

}

