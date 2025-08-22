using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Controllers;

public class RepositoriosPropietario

{
    string conexion = "Server=localhost;Database=inmobiliaria-lucasosella;User Id=sa;Password=;";

    public List<Propietario> ObtenerPropietarios()
    {
        List<Propietario> propietarios = new List<Propietario>();

        using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(conexion))
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
    
}
