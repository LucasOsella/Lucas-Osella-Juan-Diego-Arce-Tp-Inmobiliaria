using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp_inmobiliaria.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdTipoUsuario { get; set; }
        public string RolUsuario { get; set; }
    }
}
