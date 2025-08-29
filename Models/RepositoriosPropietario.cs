using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Models;

public class RepositoriosPropietario
{
    //string ConnetionString = "Server=localhost;User=root;Password=;Database=inmobiliaria-lucasosella;SslMode=none;";
    private readonly ConexionBD conexionBD;

    public RepositoriosPropietario(ConexionBD conexionBD)
    { 
        this.conexionBD = conexionBD;
    }

    

    public List<Propietario> ObtenerPropietarios()
    {
        List<Propietario> propietarios = new List<Propietario>();

        using (var connection = conexionBD.GetConnection())
        {
            var query = $@"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Email)}, {nameof(Propietario.Direccion)} FROM propietario";
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    propietarios.Add(new Propietario()
                    {
                        Id = reader.GetInt32("id"),
                        Dni = reader.GetInt32("dni"),
                        Apellido = reader.GetString("apellido"),
                        Nombre = reader.GetString("nombre"),
                        Telefono = reader.GetString("telefono"),
                        Email = reader.GetString("email"),
                        Direccion = reader.GetString("direccion")
                    });
                }
            }

        }
        return propietarios;
    }
    public void agregarPropietario(Propietario propietario)
    {

        var query = "INSERT INTO propietario (dni, apellido, nombre, telefono, email, direccion) VALUES (@dni, @apellido, @nombre, @telefono, @email, @direccion)";

        using (var connection = conexionBD.GetConnection())
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@email", propietario.Email);
                command.Parameters.AddWithValue("@direccion", propietario.Direccion);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }



    public Propietario ObtenerPropietario(int id)
    {
        
        { var query = "SELECT id, dni, apellido, nombre, telefono, email, direccion FROM propietario WHERE id=@id";
            Propietario propietario = null;

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
                        propietario = new Propietario()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetInt32("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Email = reader.GetString("email"),
                            Direccion = reader.GetString("direccion")
                        };
                    }
                }
            }
            return propietario;
        }
    }


    public void editarPropietario(Propietario propietario)
    {
        var query = @"UPDATE propietario SET dni=@dni, apellido=@apellido, nombre=@nombre, telefono=@telefono, email=@email, direccion=@direccion WHERE id=@id";
        using (var connection = conexionBD.GetConnection())
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", propietario.Id);
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@email", propietario.Email);
                command.Parameters.AddWithValue("@direccion", propietario.Direccion);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void eliminarPropietario(int id)
    {
        var query = "DELETE FROM propietario WHERE id=@id";
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
