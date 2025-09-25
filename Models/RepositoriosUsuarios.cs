using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Tp_inmobiliaria.Controllers;
using Tp_inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Tp_inmobiliaria.Models;

[Authorize]

public class RepositorioUsuario
{
    private readonly ConexionBD conexionBD;

    public RepositorioUsuario(ConexionBD conexionBD)
    {
        this.conexionBD = conexionBD;
    }

    public Usuario Login(string email)
    {
        Usuario usuario = null;

        var query = @"SELECT u.id, u.nombre_usuario, u.apellido_usuario, 
                        u.email, u.password, u.id_tipo_usuario, 
                        t.rol_usuario, u.foto
                FROM usuario u
                INNER JOIN tipo_usuario t ON u.id_tipo_usuario = t.id_tipo_usuario
                WHERE u.email = @Email AND u.activo = 1";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

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
                        Password = reader.GetString("password"), // hash guardado
                        IdTipoUsuario = reader.GetInt32("id_tipo_usuario"),
                        RolUsuario = reader.GetString("rol_usuario"),
                        foto = reader.GetString("foto")
                    };
                }
            }
        }

        return usuario;
    }
    [HttpGet]
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
    public void CrearUsuario(Usuario usuario)
    {
    using (var connection = conexionBD.GetConnection())
    {
        var query = @"INSERT INTO usuario 
            (nombre_usuario, apellido_usuario, email, password, id_tipo_usuario, activo, foto) 
            VALUES (@NombreUsuario, @ApellidoUsuario, @Email, @Password, @IdTipoUsuario, 1, @Foto)";

        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
            command.Parameters.AddWithValue("@ApellidoUsuario", usuario.ApellidoUsuario);
            command.Parameters.AddWithValue("@Email", usuario.Email);
            command.Parameters.AddWithValue("@Password", usuario.Password);
            command.Parameters.AddWithValue("@IdTipoUsuario", usuario.IdTipoUsuario);
            command.Parameters.AddWithValue("@Foto", usuario.foto ?? (object)DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
    public Usuario ObtenerUsuarioPorId(int id)
    {
        Usuario usuario = null;
        var query = @"SELECT u.id, u.nombre_usuario, u.apellido_usuario, 
                                u.email, u.password, u.id_tipo_usuario, 
                                t.rol_usuario, u.foto
                        FROM usuario u
                        INNER JOIN tipo_usuario t ON u.id_tipo_usuario = t.id_tipo_usuario
                        WHERE u.id = @Id AND u.activo = 1";

        using (var connection = conexionBD.GetConnection())
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
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
                        RolUsuario = reader.GetString("rol_usuario"),
                        foto = reader.GetString("foto")
                    };
                }
            }
        }
        return usuario;
    }
    
    public void EditarUsuario(Usuario usuario)
    {
        
        using (var connection = conexionBD.GetConnection())
        {
            var query = @"UPDATE usuario 
                        SET nombre_usuario = @NombreUsuario, 
                            apellido_usuario = @ApellidoUsuario, 
                            email = @Email, 
                            password = @Password, 
                            id_tipo_usuario = @IdTipoUsuario,
                            foto = @Foto
                        WHERE id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", usuario.Id);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@ApellidoUsuario", usuario.ApellidoUsuario);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Password", usuario.Password);
                command.Parameters.AddWithValue("@IdTipoUsuario", usuario.IdTipoUsuario);
                command.Parameters.AddWithValue("@Foto", usuario.foto ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    
    public void EliminarUsuario(int id)
    {
        using (var connection = conexionBD.GetConnection())
        {
            var query = @"UPDATE usuario 
                        SET activo = 0
                        WHERE id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}



