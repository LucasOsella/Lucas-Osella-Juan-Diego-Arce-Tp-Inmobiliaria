using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Tp_inmobiliaria.Controllers;

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
        var contrato = repo.ObtenerPorId(id);
        return View(contrato);// Busca Views/Contrato/editarContrato.cshtml
    }

    public IActionResult GuardarContrato(Contratos contrato)
    {
        if (!ModelState.IsValid)
        {
            // Si hay errores de validación, volver a la vista mostrando los mensajes
            return View("AgregarContrato", contrato);
        }
        repo.AgregarContrato(contrato);
        return RedirectToAction("Index");
    }

    public IActionResult GuardarEdicionContrato(Contratos contrato)
    {
        repo.Editar(contrato);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarContrato(int id)
    {
        repo.Eliminar(id);
        return RedirectToAction("Index");
    }
}