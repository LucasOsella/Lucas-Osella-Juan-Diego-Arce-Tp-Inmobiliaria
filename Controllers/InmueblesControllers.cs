using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers;

[Authorize]
public class InmueblesController : Controller
{
    private readonly ILogger<InmueblesController> _logger;
    private RepositoriosInmuebles repo;
    RepositoriosPropietario repoPropietario;
    RepositoriosTipoInmuebles repoTipoInmueble;

    public InmueblesController(ILogger<InmueblesController> logger, ConexionBD conexionBD)
    {
        _logger = logger;
        repo = new RepositoriosInmuebles(conexionBD);
        repoPropietario = new RepositoriosPropietario(conexionBD);
        repoTipoInmueble = new RepositoriosTipoInmuebles(conexionBD);
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
        var tipos = repoTipoInmueble.ObtenerTipoInmuebles();
        ViewBag.Tipos = tipos;
        return View(); // Views/Inmuebles/AgregarInmueble.cshtml
    }

    // Formulario para editar un inmueble
    public IActionResult EditarInmueble(int id)
    {
        var inmueble = repo.ObtenerInmueble(id);
        var propietarios = repoPropietario.ObtenerPropietarios();
        ViewBag.Propietarios = propietarios;
        var tipos = repoTipoInmueble.ObtenerTipoInmuebles();
        ViewBag.Tipos = tipos;
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

    // Filtrar inmuebles por estado
    public IActionResult Filtrar(string estado)
    {
        var lista = repo.FiltrarInmueblesPorEstado(estado);
        ViewBag.EstadoSeleccionado = estado; // Para mantener el estado seleccionado en la vista
        return View("Index", lista); // Reutiliza la vista Index para mostrar los resultados filtrados
    }

    public IActionResult FiltrarPorFecha(DateTime fecha_inicio, DateTime fecha_fin)
    {
        var lista = repo.TraerporFecha(fecha_inicio, fecha_fin);
        ViewBag.FechaInicio = fecha_inicio;
        ViewBag.FechaFin = fecha_fin;
        return View("Index", lista);
    }

}