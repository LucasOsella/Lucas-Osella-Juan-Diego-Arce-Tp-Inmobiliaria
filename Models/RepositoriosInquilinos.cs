using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Models;

public class RepositoriosInquilinos

{
    string ConnetionString = "Server=localhost;User=root;Password=;Database=inmobiliaria-lucasosella;SslMode=none;";

    public List<Inquilinos> ObtenerInquilinos()
    {
        List<Inquilinos> inquilinos = new List<Inquilinos>();

        using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnetionString))
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
    
}
