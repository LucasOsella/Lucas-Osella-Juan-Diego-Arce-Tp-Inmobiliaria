using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Models;
[Authorize]

    public class Tipo_Inmueble
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo es obligatoria")]
        [StringLength(100, ErrorMessage = "El tipo debe tener menos de 100 caracteres")]
        public string Nombre { get; set; } = "";

    }
