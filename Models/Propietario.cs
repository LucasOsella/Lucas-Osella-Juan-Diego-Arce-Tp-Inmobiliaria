using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Models;

public class Propietario
{
    public int Id { get; set; }
    public int Dni { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Email { get; set; } = "";
    public string Direccion { get; set; } = "";

}

 
