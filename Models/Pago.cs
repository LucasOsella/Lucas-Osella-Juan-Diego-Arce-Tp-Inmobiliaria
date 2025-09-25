namespace Tp_inmobiliaria.Models;

public class Pago
{
    public int id { get; set; }
    public int id_contrato { get; set; }
    public int numero_pago { get; set; }
    public DateTime fecha_pago { get; set; }
    public string? detalle { get; set; }
    public decimal importe { get; set; }
    public string estado { get; set; } = "ACTIVO"; // Valor por defecto
    public int id_usuario_creador { get; set; }
    public int? id_usuario_finalizador { get; set; }

    // Propiedades adicionales para mostrar en la vista
    public string? NombreCreador { get; set; }
    public string? ApellidoCreador { get; set; }
    public string? NombreFinalizador { get; set; }
    public string? ApellidoFinalizador { get; set; }
}