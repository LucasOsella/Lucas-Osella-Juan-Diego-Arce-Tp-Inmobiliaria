using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Models;

[Authorize]
public class RepositorioContratos
{
    private readonly ConexionBD conexionBD;

    public RepositorioContratos(ConexionBD conexionBD)
    {
        this.conexionBD = conexionBD;
    }

    // Obtener todos los contratos
    public List<Contratos> ObtenerContratos()
    {
        List<Contratos> contratos = new List<Contratos>();

        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT c.id, 
            c.id_inquilino, i.nombre AS nombre_inquilino,
            c.id_inmueble, im.direccion AS direccion_inmueble,
            c.fecha_inicio, c.fecha_fin, c.monto_mensual, 
            c.id_usuario_creador, 
            u1.nombre_usuario AS nombre_creador, u1.apellido_usuario AS apellido_creador,
            c.id_usuario_finalizador, 
            u2.nombre_usuario AS nombre_finalizador, u2.apellido_usuario AS apellido_finalizador
        FROM contrato c
        INNER JOIN inquilino i ON c.id_inquilino = i.id
        INNER JOIN inmueble im ON c.id_inmueble = im.id
        LEFT JOIN usuario u1 ON c.id_usuario_creador = u1.id
        LEFT JOIN usuario u2 ON c.id_usuario_finalizador = u2.id;
        ";

            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contratos.Add(new Contratos()
                    {
                        id = reader.GetInt32("id"),
                        id_inquilino = reader.GetInt32("id_inquilino"),
                        NombreInquilino = reader.GetString("nombre_inquilino"),
                        id_inmueble = reader.GetInt32("id_inmueble"),
                        DireccionInmueble = reader.GetString("direccion_inmueble"),
                        fecha_inicio = reader.GetDateTime("fecha_inicio"),
                        fecha_fin = reader.GetDateTime("fecha_fin"),
                        monto_mensual = reader.GetDecimal("monto_mensual"),
                        id_creador = reader.IsDBNull(reader.GetOrdinal("id_usuario_creador"))
                                        ? null
                                        : reader.GetInt32("id_usuario_creador"),
                        NombreCreador = reader.IsDBNull(reader.GetOrdinal("nombre_creador"))
                                        ? ""
                                        : reader.GetString("nombre_creador"),
                        ApellidoCreador = reader.IsDBNull(reader.GetOrdinal("apellido_creador"))
                                        ? ""
                                        : reader.GetString("apellido_creador"),
                        id_finalizador = reader.IsDBNull(reader.GetOrdinal("id_usuario_finalizador"))
                                        ? null
                                        : reader.GetInt32("id_usuario_finalizador"),
                        NombreFinalizador = reader.IsDBNull(reader.GetOrdinal("nombre_finalizador"))
                                        ? ""
                                        : reader.GetString("nombre_finalizador"),
                        ApellidoFinalizador = reader.IsDBNull(reader.GetOrdinal("apellido_finalizador"))
                                        ? ""
                                        : reader.GetString("apellido_finalizador"),
                    });
                }
            }
        }
        return contratos;
    }

    // Agregar un contrato
    public void AgregarContrato(Contratos contrato)
    {
        var query = @"INSERT INTO contrato 
                        (id_inquilino, id_inmueble, fecha_inicio, fecha_fin, monto_mensual, id_usuario_creador, id_usuario_finalizador) 
                    VALUES 
                        (@id_inquilino, @id_inmueble, @fecha_inicio, @fecha_fin, @monto_mensual, @id_creador, @id_finalizador)";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_inquilino", contrato.id_inquilino);
                command.Parameters.AddWithValue("@id_inmueble", contrato.id_inmueble);
                command.Parameters.AddWithValue("@fecha_inicio", contrato.fecha_inicio);
                command.Parameters.AddWithValue("@fecha_fin", contrato.fecha_fin);
                command.Parameters.AddWithValue("@monto_mensual", contrato.monto_mensual);
                command.Parameters.AddWithValue("@id_creador", contrato.id_creador);
                command.Parameters.AddWithValue("@id_finalizador", contrato.id_finalizador);

                connection.Open();
                command.ExecuteNonQuery();
                contrato.id = (int)command.LastInsertedId;
            }
        }
    }

    // Obtener un contrato por ID
    public Contratos ObtenerPorId(int id)
    {
        Contratos? contrato = null;

        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT id, id_inquilino, id_inmueble, fecha_inicio, fecha_fin, monto_mensual, 
                                id_usuario_creador, id_usuario_finalizador 
                        FROM contrato WHERE id = @id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    contrato = new Contratos()
                    {
                        id = reader.GetInt32("id"),
                        id_inquilino = reader.GetInt32("id_inquilino"),
                        id_inmueble = reader.GetInt32("id_inmueble"),
                        fecha_inicio = reader.GetDateTime("fecha_inicio"),
                        fecha_fin = reader.GetDateTime("fecha_fin"),
                        monto_mensual = reader.GetDecimal("monto_mensual"),
                        id_creador = reader.GetInt32("id_usuario_creador"),
                        id_finalizador = reader.GetInt32("id_usuario_finalizador")
                    };
                }
            }
        }
        return contrato!;
    }

    // Editar un contrato
    public void GuardarEditarContrato(Contratos contrato)
    {
        var query = @"UPDATE contrato 
                    SET id_inquilino=@id_inquilino, id_inmueble=@id_inmueble, fecha_inicio=@fecha_inicio, fecha_fin=@fecha_fin, 
                        monto_mensual=@monto_mensual, id_usuario_creador=@id_creador, id_usuario_finalizador=@id_finalizador 
                    WHERE id = @id";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", contrato.id);
                command.Parameters.AddWithValue("@id_inquilino", contrato.id_inquilino);
                command.Parameters.AddWithValue("@id_inmueble", contrato.id_inmueble);
                command.Parameters.AddWithValue("@fecha_inicio", contrato.fecha_inicio);
                command.Parameters.AddWithValue("@fecha_fin", contrato.fecha_fin);
                command.Parameters.AddWithValue("@monto_mensual", contrato.monto_mensual);
                command.Parameters.AddWithValue("@id_creador", contrato.id_creador);
                command.Parameters.AddWithValue("@id_finalizador", contrato.id_finalizador);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    // Eliminar contrato
    public void Eliminar(int id)
    {
        var query = "DELETE FROM contrato WHERE id = @id";

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
}
