using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp_inmobiliaria.Models
{
    public enum TipoUsuario
{
    Admin = 1,
    Empleado = 2
}
    public class Usuario
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? ApellidoUsuario { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "El rol es requerido")]
        public int? IdTipoUsuario { get; set; }
        public string? RolUsuario { get; set; }
        public int Activo { get; set; }
        public string? foto { get; set; }
    }
}
