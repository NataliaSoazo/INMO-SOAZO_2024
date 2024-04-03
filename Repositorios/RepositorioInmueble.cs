using INMO_SOAZO_2024.Models;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Diagnostics.Metrics.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace INMO_SOAZO_2024.Models;

public class RepositorioInmueble
{
    readonly string ConnectionString = "Server=localhost;Database=inmosoazo2024;User=root;Password=;";

    public RepositorioInmueble()
    {

    }

   /*public IList<Inmueble> GetInmuebles()
    {
        var inmuebles = new List<Inmueble>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"SELECT {nameof(Inmueble.Id)}, {nameof(Inmueble.Direccion)}, {nameof(Inmueble.Ambientes)}, {nameof(Inmueble.Uso)},  {nameof(Inmueble.Valor)}, {nameof(Inmueble.Disponible)}, {nameof(Inmueble.PropietarioId)} FROM inmuebles  i INNER JOIN Propietarios p ON i.PropietarioId = p.Id" +
					$" WHERE {nameof(Inmueble.Id)} = @{nameof(Inmueble.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inmuebles.Add(new Inmueble
                        {
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Ambientes = reader.GetInt16 (nameof(Inmueble.Ambientes)),
                            Uso = reader.GetString(nameof(Inmueble.Uso)),
                            Valor = reader.GetDecimal (nameof(Inmueble.Valor)),
                            Disponible = reader.GetString(nameof(Inmueble.Disponible)),
                            PropietarioId = reader.GetInt32(nameof(Inmueble.PropietarioId)),
                            Duenio = new Propietario
							{
								Id = reader.GetInt32(nameof(Propietario.Id)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
							}
                            
                        });
                     }

                }
            }
         }
         return inmuebles;
    }*/


    public IList<Inmueble> ObtenerTodos()
		{
			 var inmuebles = new List<Inmueble>();
			 using (var connection = new MySqlConnection(ConnectionString))
			{
				string sql = "SELECT i.Id, Direccion, Ambientes, Uso, Valor, Disponible, PropietarioId," +
					" p.Nombre, p.Apellido" +
					" FROM Inmuebles i INNER JOIN Propietarios p ON PropietarioId = p.Id";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						inmuebles.Add(new Inmueble
						{
							Id = reader.GetInt32(nameof(Inmueble.Id)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Ambientes = reader.GetInt32 (nameof(Inmueble.Ambientes)),
                            Uso = reader.GetString(nameof(Inmueble.Uso)),
                            Valor = reader.GetDecimal (nameof(Inmueble.Valor)),
                            Disponible = reader.GetString(nameof(Inmueble.Disponible)),
                            PropietarioId = reader.GetInt32(nameof(Inmueble.PropietarioId)),
                            Duenio = new Propietario
							{
								Id = reader.GetInt32(nameof(Propietario.Id)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            }
						});
						
					}
					connection.Close();
				}
			}
			return inmuebles;
		}
    

    public int AltaInmueble(Inmueble inmueble){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            var sql = @$"INSERT INTO inmuebles ({nameof(Inmueble.Direccion)}, {nameof(Inmueble.Ambientes)}, {nameof(Inmueble.Uso)},  {nameof(Inmueble.Valor)}, {nameof(Inmueble.Disponible)}, {nameof(Inmueble.PropietarioId)})
                                            VALUES (@{nameof(Inmueble.Direccion)}, @{nameof(Inmueble.Ambientes)}, @{nameof(Inmueble.Uso)},  @{nameof(Inmueble.Valor)}, @{nameof(Inmueble.Disponible)}, @{nameof(Inmueble.PropietarioId)});            
             SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Direccion)}", inmueble.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Ambientes)}", inmueble.Ambientes);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Valor)}",inmueble.Valor);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Disponible)}", inmueble.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.PropietarioId)}", inmueble.PropietarioId);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                inmueble.Id = id;
                connection.Close();


            }
        }
        return id;
    }

    public Inmueble? getInmueble(int id)
    {
        Inmueble? inmueble = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
          string sql = $@"SELECT i.{nameof(Inmueble.Id)}, {nameof(Inmueble.Direccion)}, {nameof(Inmueble.Ambientes)}, {nameof(Inmueble.Uso)}, {nameof(Inmueble.Valor)}, {nameof(Inmueble.Disponible)}, {nameof(Inmueble.PropietarioId)},
                      p.{nameof(Propietario.Nombre)}, p.{nameof(Propietario.Apellido)}
               FROM inmuebles i INNER JOIN propietarios p ON i.{nameof(Inmueble.PropietarioId)} = p.{nameof(Propietario.Id)}
               WHERE i.{nameof(Inmueble.Id)} = @id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inmueble = new Inmueble
                        {
                             Id = reader.GetInt32(nameof(Inmueble.Id)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Ambientes = reader.GetInt16 (nameof(Inmueble.Ambientes)),
                            Uso = reader.GetString(nameof(Inmueble.Uso)),
                            Valor = reader.GetDecimal (nameof(Inmueble.Valor)),
                            Disponible = reader.GetString(nameof(Inmueble.Disponible)),
                            PropietarioId = reader.GetInt32(nameof(Inmueble.PropietarioId)),
                            Duenio = new Propietario
							{
								Id = reader.GetInt32(nameof(Propietario.Id)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
							}
                        };
                    }
                }
            }
            
        }
        return inmueble;
    }

    public int ModificarInmueble(Inmueble inmueble){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            string sql = "UPDATE Inmuebles SET " +
					"Direccion=@direccion, Ambientes=@ambientes, Uso=@uso, Valor=@valor, Disponible=@disponible, PropietarioId=@propietarioId " +
					"WHERE Id = @id";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Direccion)}", inmueble.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Ambientes)}", inmueble.Ambientes);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Valor)}", inmueble.Valor);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Disponible)}", inmueble.Disponible);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.PropietarioId)}", inmueble.PropietarioId);
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Duenio)}", inmueble.Duenio);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                inmueble.Id = id;
                connection.Close();


            }
        }
        return 0;
    }

    public int  EliminarInmueble(int id){
        using(var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"DELETE from inmuebles WHERE {nameof(Inmueble.Id)} = @{nameof(Inmueble.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", id);
                connection.Open();
               command.ExecuteNonQuery();
               connection.Close();
            }
        }
         return 0;
    }

}