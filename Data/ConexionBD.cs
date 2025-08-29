using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp_inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Tp_inmobiliaria.Controllers;

public class ConexionBD
{
    private readonly string _connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria-lucasosella;SslMode=none;";

    public ConexionBD(IConfiguration configuration)
    {
        // Lee el connection string desde appsettings.json
        _connectionString = configuration.GetConnectionString("MiConexion");
    }
    
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    
}
