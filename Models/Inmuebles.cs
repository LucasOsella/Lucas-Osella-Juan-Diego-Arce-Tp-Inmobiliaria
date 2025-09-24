using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;

namespace Tp_inmobiliaria.Models
{
    public class Inmueble
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La Dirección es obligatoria")]
        [StringLength(100, ErrorMessage = "La Dirección debe tener menos de 100 caracteres")]
        public string Direccion { get; set; } = "";

        [Required(ErrorMessage = "El Uso es obligatorio")]
        [RegularExpression(@"^(RESIDENCIAL|COMERCIAL)$", ErrorMessage = "El Uso debe ser 'RESIDENCIAL' o 'COMERCIAL'")]
        public string Uso { get; set; } = "";

        [Required(ErrorMessage = "La cantidad de Ambientes es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de Ambientes debe ser un número positivo")]
        public int Ambientes { get; set; }

        [StringLength(100, ErrorMessage = "Las Coordenadas deben tener menos de 100 caracteres")]
        public string Coordenadas { get; set; } = "";

        [Required(ErrorMessage = "El Precio es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El Precio debe ser un número positivo")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio")]
        [RegularExpression(@"^(DISPONIBLE|SUSPENDIDO|OCUPADO)$", ErrorMessage = "El Estado debe ser 'DISPONIBLE', 'SUSPENDIDO' o 'OCUPADO'")]
        public string Estado { get; set; } = "";

        [Required(ErrorMessage = "El Propietario es obligatorio")]
        public int IdPropietario { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio")]
        public int IdTipo { get; set; }

        // Propiedades adicionales para mostrar en la vista
        public string Propietario { get; set; } = "";
        public string TipoInmueble { get; set; } = "";
    }
}