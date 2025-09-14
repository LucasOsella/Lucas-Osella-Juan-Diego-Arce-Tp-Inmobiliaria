using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;

namespace Tp_inmobiliaria.Models
{
    public class RepositorioUsuario
    {
        private readonly ConexionBD conexionBD;

        public RepositorioUsuario(ConexionBD conexionBD)
        {
            this.conexionBD = conexionBD;
        }

        public Usuario Login(string email, string password)
        {
            Usuario usuario = null;

            var query = @"SELECT u.id, u.nombre_usuario, u.apellido_usuario, 
                                u.email, u.password, u.id_tipo_usuario, 
                                t.rol_usuario
                        FROM usuario u
                        INNER JOIN tipo_usuario t ON u.id_tipo_usuario = t.id_tipo_usuario
                        WHERE u.email = @Email AND u.password = @Password AND u.activo = 1";

            using (var connection = conexionBD.GetConnection())
            {
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario = new Usuario()
                        {
                            Id = reader.GetInt32("id"),
                            NombreUsuario = reader.GetString("nombre_usuario"),
                            ApellidoUsuario = reader.GetString("apellido_usuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            IdTipoUsuario = reader.GetInt32("id_tipo_usuario"),
                            RolUsuario = reader.GetString("rol_usuario")
                        };
                    }
                }
            }

            return usuario;
        }
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (var connection = conexionBD.GetConnection())
            {
                var query = @"SELECT u.id, u.nombre_usuario, u.apellido_usuario, 
                                u.email, u.password, u.id_tipo_usuario, 
                                t.rol_usuario
                        FROM usuario u
                        INNER JOIN tipo_usuario t ON u.id_tipo_usuario = t.id_tipo_usuario
                        WHERE u.activo = 1";

                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario()
                        {
                            Id = reader.GetInt32("id"),
                            NombreUsuario = reader.GetString("nombre_usuario"),
                            ApellidoUsuario = reader.GetString("apellido_usuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            IdTipoUsuario = reader.GetInt32("id_tipo_usuario"),
                            RolUsuario = reader.GetString("rol_usuario")
                        });
                    }
                }
            }
            return usuarios;
        }
    }

    }

