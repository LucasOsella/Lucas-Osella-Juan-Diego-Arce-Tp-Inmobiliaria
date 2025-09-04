using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers;

public class InmueblesController : Controller
{
    private readonly ILogger<InmueblesController> _logger;
    private RepositoriosInmuebles repo;
     RepositoriosPropietario repoPropietario;

    public InmueblesController(ILogger<InmueblesController> logger, ConexionBD conexionBD)
    {
        _logger = logger;
        repo = new RepositoriosInmuebles(conexionBD);
        repoPropietario = new RepositoriosPropietario(conexionBD);
    }

    // Lista de inmuebles
    public IActionResult Index()
    {
        var lista = repo.ObtenerInmuebles();
        return View(lista); // Views/Inmuebles/Index.cshtml
    }

    // Formulario para agregar un inmueble
    public IActionResult AgregarInmueble()
    {
        var propietarios = repoPropietario.ObtenerPropietarios();
        ViewBag.Propietarios = propietarios;
        return View(); // Views/Inmuebles/AgregarInmueble.cshtml
    }

    // Formulario para editar un inmueble
    public IActionResult EditarInmueble(int id)
    {
        var inmueble = repo.ObtenerInmueble(id);
        return View(inmueble); // Views/Inmuebles/EditarInmueble.cshtml
    }

    // Guardar un nuevo inmueble desde la vista AgregarInmueble
    [HttpPost]
    public IActionResult GuardarInmueble(Inmueble inmueble)
    {
        if (!ModelState.IsValid)
        {
            // Devuelve a la vista si hay errores de validación
            return View("AgregarInmueble", inmueble);
        }

        repo.AgregarInmueble(inmueble);
        return RedirectToAction("Index");
    }

    // Guardar la edición de un inmueble
    [HttpPost]
    public IActionResult GuardarEdicionInmueble(Inmueble inmueble)
    {
        if (!ModelState.IsValid)
        {
            return View("EditarInmueble", inmueble);
        }

        repo.EditarInmueble(inmueble);
        return RedirectToAction("Index");
    }

    // Eliminar inmueble
    public IActionResult EliminarInmueble(int id)
    {
        repo.EliminarInmueble(id);
        return RedirectToAction("Index");
    }
}