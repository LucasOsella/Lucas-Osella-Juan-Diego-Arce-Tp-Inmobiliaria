using Microsoft.AspNetCore.Authorization;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
namespace Tp_inmobiliaria.Models;

[Authorize]
public class RepositorioPagos
{
    private readonly ConexionBD conexionBD;

    public RepositorioPagos(ConexionBD conexionBD)
    {
        this.conexionBD = conexionBD;
    }

    // Obtener todos los pagos
    public List<Pago> ObtenerPagos()
    {
        List<Pago> pagos = new List<Pago>();

        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT p.id, p.id_contrato, p.numero_pago, p.fecha_pago, p.detalle, p.importe, 
                                p.estado, p.id_usuario_creador, p.id_usuario_finalizador
                                , u1.nombre_usuario AS nombre_usuario, u1.apellido_usuario AS apellido_usuario
                                , u2.nombre_usuario AS nombre_usuario, u2.apellido_usuario AS apellido_usuario 
                        FROM pago p
                            INNER JOIN usuario u1 ON p.id_usuario_creador = u1.id
                            INNER JOIN usuario u2 ON p.id_usuario_finalizador = u2.id;";

            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pagos.Add(new Pago()
                    {
                        id = reader.GetInt32("id"),
                        id_contrato = reader.GetInt32("id_contrato"),
                        numero_pago = reader.GetInt32("numero_pago"),
                        fecha_pago = reader.GetDateTime("fecha_pago"),
                        detalle = reader.GetString("detalle"),
                        importe = reader.GetDecimal("importe"),
                        estado = reader.GetString("estado"),
                        id_usuario_creador = reader.GetInt32("id_usuario_creador"),
                        id_usuario_finalizador = reader.GetInt32("id_usuario_finalizador"),
                        NombreCreador = reader.GetString("nombre_usuario"),
                        ApellidoCreador = reader.GetString("apellido_usuario"),
                        NombreFinalizador = reader.GetString("nombre_usuario"),
                        ApellidoFinalizador = reader.GetString("apellido_usuario")
                    });
                }
            }
        }
        return pagos;
    }

    // Obtener pagos de un contrato específico
    public List<Pago> ObtenerPagosPorContrato(int idContrato)
    {
        List<Pago> pagos = new List<Pago>();

        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT id, id_contrato, numero_pago, fecha_pago, detalle, importe, 
                                estado, id_usuario_creador, id_usuario_finalizador 
                        FROM pago WHERE id_contrato = @id_contrato";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", idContrato);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pagos.Add(new Pago()
                    {
                        id = reader.GetInt32("id"),
                        id_contrato = reader.GetInt32("id_contrato"),
                        numero_pago = reader.GetInt32("numero_pago"),
                        fecha_pago = reader.GetDateTime("fecha_pago"),
                        detalle = reader.GetString("detalle"),
                        importe = reader.GetDecimal("importe"),
                        estado = reader.GetString("estado"),
                        id_usuario_creador = reader.GetInt32("id_usuario_creador"),
                        id_usuario_finalizador = reader.GetInt32("id_usuario_finalizador"),
                    });
                }
            }
        }
        return pagos;
    }

    // Obtener un pago por ID
    public Pago ObtenerPorId(int id)
    {
        Pago? pago = null;

        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT id, id_contrato, numero_pago, fecha_pago, detalle, importe, 
                                estado, id_usuario_creador, id_usuario_finalizador 
                        FROM pago WHERE id = @id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pago = new Pago()
                    {
                        id = reader.GetInt32("id"),
                        id_contrato = reader.GetInt32("id_contrato"),
                        numero_pago = reader.GetInt32("numero_pago"),
                        fecha_pago = reader.GetDateTime("fecha_pago"),
                        detalle = reader.GetString("detalle"),
                        importe = reader.GetDecimal("importe"),
                        estado = reader.GetString("estado"),
                        id_usuario_creador = reader.GetInt32("id_usuario_creador"),
                        id_usuario_finalizador = reader.GetInt32("id_usuario_finalizador")
                    };
                }
            }
        }
        return pago!;
    }

    // Agregar un pago
    public void AgregarPago(Pago pago)
    {
        var query = @"INSERT INTO pago 
                        (id_contrato, numero_pago, fecha_pago, detalle, importe, estado, id_usuario_creador, id_usuario_finalizador) 
                    VALUES 
                        (@id_contrato, @numero_pago, @fecha_pago, @detalle, @importe, @estado, @id_usuario_creador, @id_usuario_finalizador)";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", pago.id_contrato);
                command.Parameters.AddWithValue("@numero_pago", pago.numero_pago);
                command.Parameters.AddWithValue("@fecha_pago", pago.fecha_pago);
                command.Parameters.AddWithValue("@detalle", pago.detalle);
                command.Parameters.AddWithValue("@importe", pago.importe);
                command.Parameters.AddWithValue("@estado", pago.estado);
                command.Parameters.AddWithValue("@id_usuario_creador", pago.id_usuario_creador);
                command.Parameters.AddWithValue("@id_usuario_finalizador", pago.id_usuario_finalizador);

                connection.Open();
                command.ExecuteNonQuery();
                pago.id = (int)command.LastInsertedId;
            }
        }
    }

    // Editar un pago
    public void EditarPago(Pago pago)
    {
        var query = @"UPDATE pago 
                    SET id_contrato=@id_contrato, numero_pago=@numero_pago, fecha_pago=@fecha_pago, 
                        detalle=@detalle, importe=@importe, estado=@estado, 
                        id_usuario_creador=@id_usuario_creador, id_usuario_finalizador=@id_usuario_finalizador 
                    WHERE id = @id";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", pago.id);
                command.Parameters.AddWithValue("@id_contrato", pago.id_contrato);
                command.Parameters.AddWithValue("@numero_pago", pago.numero_pago);
                command.Parameters.AddWithValue("@fecha_pago", pago.fecha_pago);
                command.Parameters.AddWithValue("@detalle", pago.detalle);
                command.Parameters.AddWithValue("@importe", pago.importe);
                command.Parameters.AddWithValue("@estado", pago.estado);
                command.Parameters.AddWithValue("@id_usuario_creador", pago.id_usuario_creador);
                command.Parameters.AddWithValue("@id_usuario_finalizador", pago.id_usuario_finalizador);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    // Eliminar un pago
    public void Eliminar(int id)
    {
        var query = "UPDATE pago SET estado='ANULADO' WHERE id = @id";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public int ObtenerMesesAdeudados(int idContrato)
    {
        int mesesAdeudados = 0;
        using (var connection = conexionBD.GetConnection())
        {
            var query = "SELECT COUNT(*) FROM pago WHERE id_contrato = @idContrato AND estado = 'PENDIENTE'";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idContrato", idContrato);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mesesAdeudados = reader.GetInt32(0);
                }
            }
        }
        return mesesAdeudados;
    }
}

