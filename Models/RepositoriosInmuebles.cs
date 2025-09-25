using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Controllers;
using Microsoft.AspNetCore.Authorization;
namespace Tp_inmobiliaria.Models;

[Authorize]
public class RepositoriosInmuebles
{
    private readonly ConexionBD conexionBD;

    public RepositoriosInmuebles(ConexionBD conexionBD)
    {
        this.conexionBD = conexionBD;
    }

    //Obtener lista de inmuebles
    public List<Inmueble> ObtenerInmuebles()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT 
                            i.id,
                            i.direccion,
                            i.uso,
                            i.ambientes,
                            i.coordenadas,
                            i.precio,
                            i.estado,
                            i.id_propietario,
                            i.id_tipo,
                            CONCAT(p.apellido, ' ', p.nombre) AS propietario,
                            t.nombre AS tipo_inmueble
                        FROM inmueble i
                        INNER JOIN propietario p ON i.id_propietario = p.id
                        INNER JOIN tipo_inmueble t ON i.id_tipo = t.id;
                        ";
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    inmuebles.Add(new Inmueble()
                    {
                        Id = reader.GetInt32("id"),
                        Direccion = reader.GetString("direccion"),
                        Uso = reader.GetString("uso"),
                        Ambientes = reader.GetInt32("ambientes"),
                        Coordenadas = reader.IsDBNull(reader.GetOrdinal("coordenadas")) ? "" : reader.GetString("coordenadas"),
                        Precio = reader.GetDecimal("precio"),
                        Estado = reader.GetString("estado"),
                        IdPropietario = reader.GetInt32("id_propietario"),
                        IdTipo = reader.GetInt32("id_tipo")
                        ,
                        Propietario = reader.GetString("propietario")
                        ,
                        TipoInmueble = reader.GetString("tipo_inmueble")
                    });
                }
            }
        }
        return inmuebles;
    }

    //Obtener un inmueble por id
    public Inmueble ObtenerInmueble(int id)
    {
        Inmueble inmueble = null;
        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT id, direccion, uso, ambientes, coordenadas, precio, estado, id_propietario, id_tipo 
                        FROM inmueble WHERE id=@id";
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inmueble = new Inmueble()
                        {
                            Id = reader.GetInt32("id"),
                            Direccion = reader.GetString("direccion"),
                            Uso = reader.GetString("uso"),
                            Ambientes = reader.GetInt32("ambientes"),
                            Coordenadas = reader.IsDBNull(reader.GetOrdinal("coordenadas")) ? "" : reader.GetString("coordenadas"),
                            Precio = reader.GetDecimal("precio"),
                            Estado = reader.GetString("estado"),
                            IdPropietario = reader.GetInt32("id_propietario"),
                            IdTipo = reader.GetInt32("id_tipo")
                        };
                    }
                }
            }
        }
        return inmueble;
    }

    //Agregar un inmueble
    public void AgregarInmueble(Inmueble inmueble)
    {
        var query = @"INSERT INTO inmueble (direccion, uso, ambientes, coordenadas, precio, estado, id_propietario, id_tipo) 
                    VALUES (@direccion, @uso, @ambientes, @coordenadas, @precio, @estado, @id_propietario, @id_tipo)";
        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                command.Parameters.AddWithValue("@coordenadas", string.IsNullOrEmpty(inmueble.Coordenadas) ? (object)DBNull.Value : inmueble.Coordenadas);
                command.Parameters.AddWithValue("@precio", inmueble.Precio);
                command.Parameters.AddWithValue("@estado", inmueble.Estado);
                command.Parameters.AddWithValue("@id_propietario", inmueble.IdPropietario);
                command.Parameters.AddWithValue("@id_tipo", inmueble.IdTipo);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    //Editar un inmueble
    public void EditarInmueble(Inmueble inmueble)
    {
        var query = @"UPDATE inmueble 
                    SET direccion=@direccion, uso=@uso, ambientes=@ambientes, coordenadas=@coordenadas, 
                        precio=@precio, estado=@estado, id_propietario=@id_propietario, id_tipo=@id_tipo
                    WHERE id=@id";
        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", inmueble.Id);
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                command.Parameters.AddWithValue("@coordenadas", string.IsNullOrEmpty(inmueble.Coordenadas) ? (object)DBNull.Value : inmueble.Coordenadas);
                command.Parameters.AddWithValue("@precio", inmueble.Precio);
                command.Parameters.AddWithValue("@estado", inmueble.Estado);
                command.Parameters.AddWithValue("@id_propietario", inmueble.IdPropietario);
                command.Parameters.AddWithValue("@id_tipo", inmueble.IdTipo);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    //Eliminar un inmueble
    public void EliminarInmueble(int id)
    {
        var query = "DELETE FROM inmueble WHERE id=@id";
        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Inmueble> FiltrarInmueblesPorEstado(string estado)
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (var connection = conexionBD.GetConnection())
        {
            var query = @"SELECT 
                            i.id,
                            i.direccion,
                            i.uso,
                            i.ambientes,
                            i.coordenadas,
                            i.precio,
                            i.estado,
                            i.id_propietario,
                            i.id_tipo,
                            CONCAT(p.apellido, ' ', p.nombre) AS propietario,
                            t.nombre AS tipo_inmueble
                        FROM inmueble i
                        INNER JOIN propietario p ON i.id_propietario = p.id
                        INNER JOIN tipo_inmueble t ON i.id_tipo = t.id
                        WHERE (@estado = '' OR i.estado = @estado);
                        ";
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@estado", estado);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    inmuebles.Add(new Inmueble()
                    {
                        Id = reader.GetInt32("id"),
                        Direccion = reader.GetString("direccion"),
                        Uso = reader.GetString("uso"),
                        Ambientes = reader.GetInt32("ambientes"),
                        Coordenadas = reader.IsDBNull(reader.GetOrdinal("coordenadas")) ? "" : reader.GetString("coordenadas"),
                        Precio = reader.GetDecimal("precio"),
                        Estado = reader.GetString("estado"),
                        IdPropietario = reader.GetInt32("id_propietario"),
                        IdTipo = reader.GetInt32("id_tipo")
                        ,
                        Propietario = reader.GetString("propietario")
                        ,
                        TipoInmueble = reader.GetString("tipo_inmueble")
                    });
                }
            }
        }
        return inmuebles;
    }
    
    public void CambiarEstadoInmueble(int id, string nuevoEstado)
    {
        var query = "UPDATE inmueble SET estado=@estado WHERE id=@id";
        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@estado", nuevoEstado);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}