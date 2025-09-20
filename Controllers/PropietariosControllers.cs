using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Controllers;
public class PropietariosController : Controller
{
    private readonly ILogger<PropietariosController> _logger;
    private RepositoriosPropietario repo;

    public PropietariosController(ILogger<PropietariosController> logger, ConexionBD conexionBD)
    {
        _logger = logger;
        repo = new RepositoriosPropietario(conexionBD);
    }


    public IActionResult Index()
    {
        var lista = repo.ObtenerPropietarios();
        return View(lista); // Busca Views/Propietarios/Index.cshtml
    }
    //Renderiza la vista de Agregar Propietario
    public IActionResult AgregarPropietario()
    {
        // Busca Views/Propietarios/agregarPropietario.cshtml
        return View(); 
    }
    //Renderiza al vista de Editar Propietario
    public IActionResult EditarPropietario(int id)
    {
        var propietario = repo.ObtenerPropietario(id);
        return View(propietario);// Busca Views/Propietarios/editarPropietario.cshtml
    }

    //Guarda el propietario desde la visa AgregarPropietario
    public IActionResult GuardarPropietario(Propietario propietario)
    {
        if (!ModelState.IsValid)
        { 
         // Si hay errores de validación, volver a la vista mostrando los mensajes
            return View("AgregarPropietario", propietario);
        }
        repo.agregarPropietario(propietario);
        return RedirectToAction("Index");
    }

    //Guarda el propietario editado desde la vista Editar Propietario.
    public IActionResult GuardarEdicionPropietario(Propietario propietario)
    {
        repo.editarPropietario(propietario);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarPropietario(int id)
    {
        repo.eliminarPropietario(id);
        return RedirectToAction("Index");
    }

}

