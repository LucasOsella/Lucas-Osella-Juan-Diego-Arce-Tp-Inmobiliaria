using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp_inmobiliaria.Models;

public class Inquilinos
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El DNI es obligatorio")]
    [Range(1000000, 99999999, ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos")]
    public int Dni { get; set; }

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El Nombre debe tener menos de 100 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener letras y espacios")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El Telefono es obligatorio")]
    [Phone(ErrorMessage = "Ingrese un numoero valido")]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "El Email es obligatorio")]
    [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "La Direccion es obligatorio")]
    [StringLength(100)]
    public string Direccion { get; set; } = "";

}

