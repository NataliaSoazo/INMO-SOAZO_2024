using INMO_SOAZO_2024.Models;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Diagnostics.Metrics.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace INMO_SOAZO_2024.Models;

public class RepositorioInquilino
{
    readonly string ConnectionString = "Server=localhost;Database=inmosoazo2024;User=root;Password=;";

    public RepositorioInquilino()
    {

    }

    public IList<Inquilino> GetInquilinos()
    {
        var inquilinos = new List<Inquilino>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"SELECT {nameof(Inquilino.Id)}, {nameof(Inquilino.Dni)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)},  {nameof(Inquilino.Email)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.Domicilio)}, {nameof(Inquilino.Ciudad)} FROM inquilinos";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inquilinos.Add(new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString (nameof(Inquilino.Apellido)),
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                            Email = reader.GetString (nameof(Inquilino.Email)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),
                            Ciudad = reader.GetString(nameof(Inquilino.Ciudad)),

                        });
                     }

                }
            }
         }
         return inquilinos;
    }

    public int AltaInquilino(Inquilino inquilino){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            var sql = @$"INSERT INTO inquilinos ({nameof(Inquilino.Dni)},{nameof(Inquilino.Nombre)},{nameof(Inquilino.Apellido)},{nameof(Inquilino.Email)},{nameof(Inquilino.Telefono)},{nameof(Inquilino.Domicilio)},{nameof(Inquilino.Ciudad)})
                                            VALUES (@{nameof(Inquilino.Dni)}, @{nameof(Inquilino.Nombre)}, @{nameof(Inquilino.Apellido)}, @{nameof(Inquilino.Email)}, @{nameof(Inquilino.Telefono)}, @{nameof(Inquilino.Domicilio)}, @{nameof(Inquilino.Ciudad)});            
             SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}",inquilino.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Dni)}", inquilino.Dni);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Email)}", inquilino.Email);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Telefono)}",inquilino.Telefono);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Domicilio)}",inquilino.Domicilio);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Ciudad)}", inquilino.Ciudad);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                inquilino.Id = id;
                connection.Close();


            }
        }
        return id;
    }

    public Inquilino? getInquilino(int id)
    {
        Inquilino? inquilino = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"SELECT {nameof(Inquilino.Id)},{nameof(Inquilino.Dni)},{nameof(Inquilino.Nombre)},{nameof(Inquilino.Apellido)},{nameof(Inquilino.Email)},{nameof(Inquilino.Telefono)},{nameof(Inquilino.Domicilio)},{nameof(Inquilino.Ciudad)} 
            FROM inquilinos WHERE {nameof(Inquilino.Id)} = @{nameof(Inquilino.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Id)}", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                             Dni = reader.GetString(nameof(Propietario.Dni)),
                            Email = reader.GetString(nameof(Inquilino.Email)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            Domicilio = reader.GetString(nameof(Inquilino.Domicilio)),
                            Ciudad = reader.GetString(nameof(Inquilino.Ciudad)),
                        };
                    }
                }
            }
            
        }
        return inquilino;
    }

    public int ModificarInquilino(Inquilino inquilino){
         using (var connection = new MySqlConnection(ConnectionString))
        {   var sql = @$"UPDATE inquilinos 
            SET {nameof(Inquilino.Nombre)} = @{nameof(Inquilino.Nombre)},
                {nameof(Inquilino.Apellido)} = @{nameof(Inquilino.Apellido)},
                {nameof(Inquilino.Dni)} = @{nameof(Inquilino.Dni)},
                {nameof(Inquilino.Email)} = @{nameof(Inquilino.Email)},
                {nameof(Inquilino.Telefono)} = @{nameof(Inquilino.Telefono)},
                {nameof(Inquilino.Domicilio)} = @{nameof(Inquilino.Domicilio)},
                {nameof(Inquilino.Ciudad)} = @{nameof(Inquilino.Ciudad)}
            WHERE {nameof(Inquilino.Id)} = @{nameof(Inquilino.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {   command.Parameters.AddWithValue($"@{nameof(Inquilino.Id)}", inquilino.Id);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}",inquilino.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Dni)}", inquilino.Dni);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Email)}", inquilino.Email);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Telefono)}",inquilino.Telefono);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Domicilio)}",inquilino.Domicilio);
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Ciudad)}", inquilino.Ciudad);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return 0;
    }

    public int  Eliminar(int id){
        using(var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"DELETE from inquilinos WHERE {nameof(Inquilino.Id)} = @{nameof(Inquilino.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inquilino.Id)}", id);
                connection.Open();
               command.ExecuteNonQuery();
               connection.Close();
            }
        }
         return 0;
    }

}