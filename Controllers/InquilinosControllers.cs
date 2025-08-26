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
}

