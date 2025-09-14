using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Models;

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
            var query = @"SELECT id, id_inquilino, id_inmueble, fecha_inicio, fecha_fin, monto_mensual, 
                                id_usuario_creador, id_usuario_finalizador 
                        FROM contrato";

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
                        id_inmueble = reader.GetInt32("id_inmueble"),
                        fecha_inicio = reader.GetDateTime("fecha_inicio"),
                        fecha_fin = reader.GetDateTime("fecha_fin"),
                        monto_mensual = reader.GetDouble("monto_mensual"),
                        id_creador = reader.GetInt32("id_usuario_creador"),
                        id_finalizador = reader.GetInt32("id_usuario_finalizador")
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
                        monto_mensual = reader.GetDouble("monto_mensual"),
                        id_creador = reader.GetInt32("id_usuario_creador"),
                        id_finalizador = reader.GetInt32("id_usuario_finalizador")
                    };
                }
            }
        }
        return contrato!;
    }

    // Editar un contrato
    public void Editar(Contratos contrato)
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
