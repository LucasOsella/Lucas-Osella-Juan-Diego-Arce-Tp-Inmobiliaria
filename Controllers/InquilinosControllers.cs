using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers;

public class InquilinosController : Controller
{
    private readonly ILogger<InquilinosController> _logger;
    private RepositoriosInquilinos repo;

    public InquilinosController(ILogger<InquilinosController> logger)
    {
        _logger = logger;
        repo = new RepositoriosInquilinos();
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerInquilinos();
        return View(lista); // Busca Views/Inquilinos/Index.cshtml
    }

    public IActionResult AgregarInquilino()
    {
        return View(); // Busca Views/Inquilinos/agregarInquilino.cshtml
    }

       public IActionResult EditarInquilino(int id)
    {
        var inquilinos = repo.obtenerInquilino(id);
        return View(inquilinos);// Busca Views/Propietarios/editarInquilino.cshtml
    }

    //Guarda el inquilino desde la visa AgregarInquilino
    public IActionResult GuardarInquilino(Inquilinos inquilinos)
    {
        repo.agregarInquilino(inquilinos);
        return RedirectToAction("Index");
    }

    //Guarda el inquilino editado desde la vista Editar Inquilino.
    public IActionResult GuardarEdicionInquilinos(Inquilinos inquilinos)
    {
        repo.editarInquilino(inquilinos);
        return RedirectToAction("Index");
    }

}

