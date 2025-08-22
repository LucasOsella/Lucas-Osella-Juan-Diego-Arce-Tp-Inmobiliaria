using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers;

public class PropietariosController : Controller
{   
    private readonly ILogger<PropietariosController> _logger;
    private RepositoriosPropietario repo;

    public PropietariosController(ILogger<PropietariosController> logger)
    {
        _logger = logger;
        repo = new RepositoriosPropietario();
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerPropietarios();
        return View(lista); // Busca Views/Propietarios/Index.cshtml
    }
}

