using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Models;
[Authorize]
public class RepositoriosInquilinos

{
    //string ConnetionString = "Server=localhost;User=root;Password=;Database=inmobiliaria-lucasosella;SslMode=none;";
    private readonly ConexionBD conexionBD;

    public RepositoriosInquilinos(ConexionBD conexionBD)
    { 
        this.conexionBD = conexionBD;
    }



    public List<Inquilinos> ObtenerInquilinos()
    {
        List<Inquilinos> inquilinos = new List<Inquilinos>();
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        using (var connection = conexionBD.GetConnection())
        {
            var query = $@"SELECT {nameof(Inquilinos.Id)}, {nameof(Inquilinos.Dni)},{nameof(Inquilinos.Nombre)}, {nameof(Inquilinos.Telefono)}, {nameof(Inquilinos.Email)}, {nameof(Inquilinos.Direccion)} FROM inquilino";
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    inquilinos.Add(new Inquilinos()
                    {
                        Id = reader.GetInt32("id"),
                        Dni = reader.GetInt32("dni"),
                        Nombre = reader.GetString("nombre"),
                        Telefono = reader.GetString("telefono"),
                        Email = reader.GetString("email"),
                        Direccion = reader.GetString("direccion")
                    });
                }
            }

        }
        return inquilinos;
    }

    public void agregarInquilino(Inquilinos inquilino)
    {
        var query = @"INSERT INTO inquilino (dni, nombre, telefono, email, direccion) VALUES (@dni, @nombre, @telefono, @email, @direccion)";
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        using (var connection = conexionBD.GetConnection())
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@dni", inquilino.Dni);
                command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
                command.Parameters.AddWithValue("@email", inquilino.Email);
                command.Parameters.AddWithValue("@direccion", inquilino.Direccion);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void editarInquilino(Inquilinos inquilino)
    {
        var query = @"UPDATE inquilino SET dni=@dni, nombre=@nombre, telefono=@telefono, email=@email, direccion=@direccion WHERE id=@id";
        // using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        using (var connection = conexionBD.GetConnection())
        {
            using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", inquilino.Id);
                command.Parameters.AddWithValue("@dni", inquilino.Dni);
                command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
                command.Parameters.AddWithValue("@email", inquilino.Email);
                command.Parameters.AddWithValue("@direccion", inquilino.Direccion);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public Inquilinos obtenerInquilino(int id)
    {
        var query = "SELECT id, dni, nombre, telefono, email, direccion FROM inquilino WHERE id=@id";
        Inquilinos inquilino = null;

        //using (var connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        using (var connection = conexionBD.GetConnection())
        using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    inquilino = new Inquilinos()
                    {
                        Id = reader.GetInt32("id"),
                        Dni = reader.GetInt32("dni"),
                        Nombre = reader.GetString("nombre"),
                        Telefono = reader.GetString("telefono"),
                        Email = reader.GetString("email"),
                        Direccion = reader.GetString("direccion")
                    };
                }
            }
        }
        return inquilino;
    }

    public void eliminarInquilino(int id)
    {
        var query = "DELETE FROM inquilino WHERE id=@id";
        //using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
        using (var connection = conexionBD.GetConnection())
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
