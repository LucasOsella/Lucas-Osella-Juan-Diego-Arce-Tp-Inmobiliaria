using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers;

public class ConexionBD : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
