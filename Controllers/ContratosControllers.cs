using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Tp_inmobiliaria.Controllers;

[Authorize]
public class ContratosController : Controller
{
    private readonly ILogger<ContratosController> _logger;
    private RepositorioContratos repo;
    private RepositoriosInmuebles repoInmuebles;
    private RepositoriosInquilinos repoInquilinos;
    private RepositorioUsuario repoUsuarios;

    public ContratosController(ILogger<ContratosController> logger, ConexionBD conexionBD)
    {
        _logger = logger;
        repo = new RepositorioContratos(conexionBD);
        repoInmuebles = new RepositoriosInmuebles(conexionBD);
        repoInquilinos = new RepositoriosInquilinos(conexionBD);
        repoUsuarios = new RepositorioUsuario(conexionBD);
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerContratos();
        return View(lista); // Busca Views/Contrato/Index.cshtml
    }

    public IActionResult AgregarContrato()
    {
        // Busca Views/Contrato/agregarContrato.cshtml
        var inmuebles = repoInmuebles.ObtenerInmuebles();
        ViewBag.Inmuebles = inmuebles;
        var inquilinos = repoInquilinos.ObtenerInquilinos();
        ViewBag.Inquilinos = inquilinos;
        var usuarios = repoUsuarios.ObtenerUsuarios();
        ViewBag.Usuarios = usuarios;
        return View();
    }

    public IActionResult EditarContrato(int id)
    {
        var inmuebles = repoInmuebles.ObtenerInmuebles();
        ViewBag.Inmuebles = inmuebles;
        var inquilinos = repoInquilinos.ObtenerInquilinos();
        ViewBag.Inquilinos = inquilinos;
        var usuarios = repoUsuarios.ObtenerUsuarios();
        ViewBag.Usuarios = usuarios;
        var contrato = repo.ObtenerPorId(id);
        return View(contrato);// Busca Views/Contrato/editarContrato.cshtml
    }

/*************  ✨ Windsurf Command ⭐  *************/
/// <summary>
/// Guarda el contrato desde la vista AgregarContrato.
/// </summary>
///
/*******  c5de2040-aa5a-497c-a5ab-1ace2dccca0d  *******/    [HttpPost]
    public IActionResult GuardarContrato(Contratos contrato)
    {
        if (!ModelState.IsValid)
        {
            // Si hay errores de validación, volver a la vista mostrando los mensajes
            return View("AgregarContrato", contrato);
        }
        bool exito = repo.ExisteContratoSolapado(contrato);

        if (exito)
        {
            var inmuebles = repoInmuebles.ObtenerInmuebles();
            ViewBag.Inmuebles = inmuebles;
            var inquilinos = repoInquilinos.ObtenerInquilinos();
            ViewBag.Inquilinos = inquilinos;
            var usuarios = repoUsuarios.ObtenerUsuarios();
            ViewBag.Usuarios = usuarios;
            ModelState.AddModelError(string.Empty, "Un inquilino ya tiene un contrato activo en las fechas seleccionadas.");
            return View("AgregarContrato", contrato);
        }

        repoInmuebles.CambiarEstadoInmueble(contrato.id_inmueble, "OCUPADO");
        repo.AgregarContrato(contrato);
        return RedirectToAction("Index");
    }
    public IActionResult GuardarEdicionContrato(Contratos contrato)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Inmuebles = repoInmuebles.ObtenerInmuebles();
            ViewBag.Inquilinos = repoInquilinos.ObtenerInquilinos();
            ViewBag.Usuarios = repoUsuarios.ObtenerUsuarios();
            return View("EditarContrato", contrato);
        }
            var inmuebles = repoInmuebles.ObtenerInmuebles();
            ViewBag.Inmuebles = inmuebles;
            var inquilinos = repoInquilinos.ObtenerInquilinos();
            ViewBag.Inquilinos = inquilinos;
            var usuarios = repoUsuarios.ObtenerUsuarios();
            ViewBag.Usuarios = usuarios;
        repoInmuebles.CambiarEstadoInmueble(contrato.id_inmueble, "OCUPADO");               
        repo.GuardarEditarContrato(contrato);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarContrato(int id)
    {
        repoInmuebles.CambiarEstadoInmueble(repo.ObtenerPorId(id).id_inmueble, "DISPONIBLE");
        repo.Eliminar(id);
        return RedirectToAction("Index");
    }

    public IActionResult Renovar(int id)
    {
        var contrato = repo.ObtenerPorId(id);
        var inmuebles = repoInmuebles.ObtenerInmuebles();
        ViewBag.Inmuebles = inmuebles;
        var inquilinos = repoInquilinos.ObtenerInquilinos();
        ViewBag.Inquilinos = inquilinos;
        var usuarios = repoUsuarios.ObtenerUsuarios();
        ViewBag.Usuarios = usuarios;
        contrato.fecha_inicio = contrato.fecha_fin.AddDays(1);
        contrato.fecha_fin = contrato.fecha_fin.AddMonths(6);
        return View("RenovarContrato", contrato); // Busca Views/Contrato/editarContrato.cshtml
    }
}