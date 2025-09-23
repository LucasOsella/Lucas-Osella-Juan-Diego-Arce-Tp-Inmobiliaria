namespace Tp_inmobiliaria.Models;

public class Pago
{
    public int id { get; set; }
    public int id_contrato { get; set; }
    public int numero_pago { get; set; }
    public DateTime fecha_pago { get; set; }
    public string? detalle { get; set; }   // Ahora puede ser null
    public decimal importe { get; set; }
    public string estado { get; set; } = "ACTIVO"; // Valor por defecto
    public int id_usuario_creador { get; set; }
    public int? id_usuario_finalizador { get; set; }  // Ahora es nullable
}