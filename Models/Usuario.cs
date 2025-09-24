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
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int IdTipoUsuario { get; set; }
        public string RolUsuario { get; set; }
        public int Activo { get; set; }
        public string? foto { get; set; }
    }
}
