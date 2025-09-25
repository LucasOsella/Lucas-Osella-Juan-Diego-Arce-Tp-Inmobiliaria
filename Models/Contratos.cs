using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp_inmobiliaria.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Contratos
    {
        public int id { get; set; } // este es PK, no suele necesitar validación

        [Required(ErrorMessage = "El inquilino es obligatorio.")]
        public int id_inquilino { get; set; }

        [Required(ErrorMessage = "El inmueble es obligatorio.")]
        public int id_inmueble { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_inicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime fecha_fin { get; set; }

        [Required(ErrorMessage = "El monto mensual es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "El monto mensual debe ser mayor a 0.")]
        public decimal monto_mensual { get; set; }

        [Required(ErrorMessage = "Debe registrarse quién creó el contrato.")]
        public int? id_creador { get; set; }

        // este puede ser null hasta que alguien finalice el contrato
        public int? id_finalizador { get; set; }
        public string? NombreInquilino { get; set; }
        public string? DireccionInmueble { get; set; }
        public string? NombreCreador { get; set; }
        public string? ApellidoCreador { get; set; }
        public string? NombreFinalizador { get; set; }
        public string? ApellidoFinalizador { get; set; }

    }
}