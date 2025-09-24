using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Controllers;
[Authorize]

public class PagosController : Controller
{
    private readonly ILogger<PagosController> _logger;
    private RepositorioPagos repo;
    private RepositorioContratos repoContratos;
    private RepositorioUsuario repoUsuarios;

    public PagosController(ILogger<PagosController> logger, ConexionBD conexionBD)
    {
        _logger = logger;
        repo = new RepositorioPagos(conexionBD);
        repoContratos = new RepositorioContratos(conexionBD);
        repoUsuarios = new RepositorioUsuario(conexionBD);
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerPagos();
        return View(lista); // Busca Views/Pagos/Index.cshtml
    }

    public IActionResult AgregarPago()
    {
        var contratos = repoContratos.ObtenerContratos();
        ViewBag.Contratos = contratos;
        var usuarios = repoUsuarios.ObtenerUsuarios();
        ViewBag.Usuarios = usuarios;
        return View();
    }

    [HttpPost]
    public IActionResult GuardarPago(Pago pago)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Contratos = repoContratos.ObtenerContratos();
            ViewBag.Usuarios = repoUsuarios.ObtenerUsuarios();
            return View("AgregarPago", pago);
        }

        repo.AgregarPago(pago);
        return RedirectToAction("Index");
    }

    public IActionResult EditarPago(int id)
    {
        var pago = repo.ObtenerPorId(id);
        var contratos = repoContratos.ObtenerContratos();
        ViewBag.Contratos = contratos;
        var usuarios = repoUsuarios.ObtenerUsuarios();
        ViewBag.Usuarios = usuarios;
        return View(pago); // Busca Views/Pagos/EditarPago.cshtml
    }

    [HttpPost]
    public IActionResult GuardarEdicionPago(Pago pago)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Contratos = repoContratos.ObtenerContratos();
            ViewBag.Usuarios = repoUsuarios.ObtenerUsuarios();
            return View("EditarPago", pago);
        }

        repo.EditarPago(pago);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarPago(int id)
    {
        repo.Eliminar(id);
        return RedirectToAction("Index");
    }
}