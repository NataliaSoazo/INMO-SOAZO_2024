using INMO_SOAZO_2024.Models;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Diagnostics.Metrics.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace INMO_SOAZO_2024.Models;

public class RepositorioPropietario
{
    readonly string ConnectionString = "Server=localhost;Database=inmosoazo2024;User=root;Password=;";

    public RepositorioPropietario()
    {

    }

    public IList<Propietario> GetPropietarios()
    {
        var propietarios = new List<Propietario>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)},  {nameof(Propietario.Email)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Domicilio)}, {nameof(Propietario.Ciudad)} FROM propietarios";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propietarios.Add(new Propietario
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString (nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni)),
                            Email = reader.GetString (nameof(Propietario.Email)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Domicilio = reader.GetString(nameof(Propietario.Domicilio)),
                            Ciudad = reader.GetString(nameof(Propietario.Ciudad)),

                        });
                     }

                }
            }
         }
         return propietarios;
    }

    public int AltaPropietario(Propietario propietario){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            var sql = @$"INSERT INTO propietarios ({nameof(Propietario.Nombre)},{nameof(Propietario.Apellido)},{nameof(Propietario.Dni)},{nameof(Propietario.Email)},{nameof(Propietario.Telefono)},{nameof(Propietario.Domicilio)},{nameof(Propietario.Ciudad)})
                                            VALUES (@{nameof(Propietario.Nombre)},@{nameof(Propietario.Apellido)},@{nameof(Propietario.Dni)},@{nameof(Propietario.Email)},@{nameof(Propietario.Telefono)},@{nameof(Propietario.Domicilio)},@{nameof(Propietario.Ciudad)});            
             SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Dni)}", propietario.Dni);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Email)}", propietario.Email);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Telefono)}", propietario.Telefono);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Domicilio)}", propietario.Domicilio);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Ciudad)}", propietario.Ciudad);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                propietario.Id = id;
                connection.Close();


            }
        }
        return id;
    }

    public Propietario? getPropietario(int id)
    {
        Propietario? propietario = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"SELECT {nameof(Propietario.Id)},{nameof(Propietario.Nombre)},{nameof(Propietario.Apellido)},{nameof(Propietario.Dni)},{nameof(Propietario.Email)},{nameof(Propietario.Telefono)},{nameof(Propietario.Domicilio)},{nameof(Propietario.Ciudad)} 
            FROM propietarios WHERE {nameof(Propietario.Id)} = @{nameof(Propietario.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Propietario.Id)}", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni)),
                            Email = reader.GetString(nameof(Propietario.Email)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Domicilio = reader.GetString(nameof(Propietario.Domicilio)),
                            Ciudad = reader.GetString(nameof(Propietario.Ciudad)),
                        };
                    }
                }
            }
            
        }
        return propietario;
    }

    public int ModificarPropietario(Propietario propietario){
        using (var connection = new MySqlConnection(ConnectionString)){
            var sql = @$"UPDATE propietarios 
            SET {nameof(Propietario.Nombre)} = @{nameof(Propietario.Nombre)},
            {nameof(Propietario.Apellido)} = @{nameof(Propietario.Apellido)},
            {nameof(Propietario.Dni)} = @{nameof(Propietario.Dni)},
            {nameof(Propietario.Email)} = @{nameof(Propietario.Email)},
            {nameof(Propietario.Telefono)} = @{nameof(Propietario.Telefono)},
            {nameof(Propietario.Domicilio)} = @{nameof(Propietario.Domicilio)},
            {nameof(Propietario.Ciudad)} = @{nameof(Propietario.Ciudad)}
            WHERE {nameof(Propietario.Id)} = @{nameof(Propietario.Id)} ";     
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Propietario.Id)}", propietario.Id);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Dni)}", propietario.Dni);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Email)}", propietario.Email);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Telefono)}", propietario.Telefono);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Domicilio)}", propietario.Domicilio);
                command.Parameters.AddWithValue($"@{nameof(Propietario.Ciudad)}", propietario.Ciudad);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return 0;
    }

    public int  EliminarPersona(int id){
        using(var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"DELETE from propietarios WHERE {nameof(Propietario.Id)} = @{nameof(Propietario.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Propietario.Id)}", id);
                connection.Open();
               command.ExecuteNonQuery();
               connection.Close();
            }
        }
         return 0;
    }

}