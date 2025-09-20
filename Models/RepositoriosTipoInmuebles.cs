using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Models;
[Authorize]
public class RepositoriosTipoInmuebles
{
    //string ConnetionString = "Server=localhost;User=root;Password=;Database=inmobiliaria-lucasosella;SslMode=none;";
    private readonly ConexionBD conexionBD;

    public RepositoriosTipoInmuebles(ConexionBD conexionBD)
    { 
        this.conexionBD = conexionBD;
    }

    

    public List<Tipo_Inmueble> ObtenerTipoInmuebles()
    {
        List<Tipo_Inmueble> TipoInmueble = new List<Tipo_Inmueble>();

        using (var connection = conexionBD.GetConnection())
        {
            var query = $@"SELECT {nameof(Tipo_Inmueble.Id)}, {nameof(Tipo_Inmueble.Nombre)} FROM tipo_inmueble";
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TipoInmueble.Add(new Tipo_Inmueble()
                    {
                        Id = reader.GetInt32("id"),
                        Nombre = reader.GetString("nombre"),
                    });
                }
            }

        }
        return TipoInmueble;
    }
    public void AgregarTipoInmueble(Tipo_Inmueble Tipo_Inmueble)
    {

        var query = "INSERT INTO tipo_inmueble (nombre) VALUES (@nombre)";

        using (var connection = conexionBD.GetConnection())
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", Tipo_Inmueble.Nombre);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }



    public Tipo_Inmueble ObtenerTipo(int id)
    {
        
        { var query = "SELECT id, nombre FROM tipo_inmueble WHERE id=@id";
            Tipo_Inmueble tipo_inmueble = null;

            using (var connection = conexionBD.GetConnection())
            //using (var connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
            using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tipo_inmueble = new Tipo_Inmueble()
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                        };
                    }
                }
            }
            return tipo_inmueble;
        }
    }


    public void editarTipo(Tipo_Inmueble tipo_inmueble)
    {
        var query = @"UPDATE tipo_inmueble SET nombre=@nombre WHERE id=@id";
        using (var connection = conexionBD.GetConnection())
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {      
                command.Parameters.AddWithValue("@nombre", tipo_inmueble.Nombre);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void eliminarTipo(int id)
    {
        var query = "DELETE FROM tipo_inmueble WHERE id=@id";
        using (var connection = conexionBD.GetConnection())
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    
    
}
