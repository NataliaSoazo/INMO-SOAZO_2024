using MySql.Data.MySqlClient;

namespace INMO_SOAZO_2024.Models;

public class RepositorioContrato
{
    readonly string ConnectionString = "Server=localhost;Database=inmosoazo2024;User=root;Password=;";

    public RepositorioContrato()
    {

    }

    public IList<Contrato> GetContratos()
    {
        var contratos = new List<Contrato>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            string sql = $@"SELECT i.{nameof(Contrato.Id)}, {nameof(Contrato.FechaInicio)}, {nameof(Contrato.FechaTerm)}, {nameof(Contrato.IdInquilino)},
                      p.{nameof(Inquilino.Nombre)}, p.{nameof(Inquilino.Apellido)}, {nameof(Contrato.IdInmueble)}, m.{nameof(Inmueble.Direccion)}
                FROM Contratos i 
                INNER JOIN Inquilinos p ON i.{nameof(Contrato.IdInquilino)} = p.{nameof(Inquilino.Id)}
                INNER JOIN Inmuebles m ON i.{nameof(Contrato.IdInmueble)} = m.{nameof(Inmueble.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contratos.Add(new Contrato
                        {
                            Id = reader.GetInt32(nameof(Contrato.Id)),
							FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
							FechaTerm = reader.GetDateTime(nameof(Contrato.FechaTerm)),
							IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
							

							Locatario = new Inquilino
                            {
								Nombre = reader.GetString(nameof(Inquilino.Nombre)),
								Apellido = reader.GetString(nameof(Inquilino.Apellido)),
			
                            },
							IdInmueble = reader.GetInt32(nameof(Inmueble.Id)),
							Datos = new Inmueble
							{
								
								Direccion = reader.GetString(nameof(Inmueble.Direccion))

							}
                            
                        });
                     }

                }
            }
         }
         return contratos;
    }


    public IList<Inmueble> ObtenerTodos()
		{
			 var inmuebles = new List<Inmueble>();
			 using (var connection = new MySqlConnection(ConnectionString))
			{
				string sql = $@"SELECT i.{nameof(Inmueble.Id)}, {nameof(Inmueble.Direccion)}, {nameof(Inmueble.Ambientes)}, {nameof(Inmueble.Uso)}, {nameof(Inmueble.Valor)}, {nameof(Inmueble.Disponible)}, {nameof(Inmueble.PropietarioId)},
                      p.{nameof(Propietario.Nombre)}, p.{nameof(Propietario.Apellido)}
               FROM Inmuebles i INNER JOIN Propietarios p ON i.{nameof(Inmueble.PropietarioId)} = p.{nameof(Propietario.Id)}";
				using (var command = new MySqlCommand(sql, connection))
				{
					
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
    

    public int AltaContrato(Contrato contrato){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            string sql = $@"INSERT INTO Contratos ( {nameof(Contrato.FechaInicio)}, {nameof(Contrato.FechaTerm)}, {nameof(Contrato.IdInquilino)}, {nameof(Contrato.IdInmueble)}) 
                VALUES (@fechaInicio, @fechaTerm, @idInquilino, @idInmueble);           
                SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaInicio)}", contrato.FechaInicio);
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaTerm)}", contrato.FechaTerm);
                command.Parameters.AddWithValue($"@{nameof(Contrato.IdInquilino)}", contrato.IdInmueble);
                command.Parameters.AddWithValue($"@{nameof(Contrato.IdInmueble)}",contrato.IdInmueble);
               
                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                contrato.Id = id;
                connection.Close();


            }
        }
        return id;
    }

    public Contrato? getContrato(int id)
    {
        Contrato? contrato = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
                string sql = $@"SELECT i.{nameof(Contrato.Id)}, {nameof(Contrato.FechaInicio)}, {nameof(Contrato.FechaTerm)}, {nameof(Contrato.IdInquilino)}, 
                      p.{nameof(Inquilino.Nombre)}, p.{nameof(Inquilino.Apellido)}, {nameof(Contrato.IdInmueble)}, m.{nameof(Inmueble.Direccion)}
                FROM Contratos i INNER JOIN Inquilinos p ON i.{nameof(Contrato.IdInquilino)} = p.{nameof(Inquilino.Id)}
                                  INNER JOIN Inmuebles m ON i.{nameof(Contrato.IdInmueble)} = m.{nameof(Inmueble.Id)}
                WHERE i.{nameof(Contrato.Id)} = @id";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrato = new Contrato
                        {
                            Id = reader.GetInt32(nameof(Contrato.Id)),
							FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
							FechaTerm = reader.GetDateTime(nameof(Contrato.FechaTerm)),
							IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
							

							Locatario = new Inquilino
                            {
								Nombre = reader.GetString(nameof(Inquilino.Nombre)),
								Apellido = reader.GetString(nameof(Inquilino.Apellido)),
			
                            },
							IdInmueble = reader.GetInt32(nameof(Inmueble.Id)),
							Datos = new Inmueble
							{
								
								Direccion = reader.GetString(nameof(Inmueble.Direccion))

							}
                        };
                    }
                }
            }
            
        }
        return contrato;
    }

    public int ModificarContrato(Contrato contrato){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            string sql = $@"UPDATE Contratos SET 
                    {nameof(Contrato.FechaInicio)} = @fechaInicio, 
                    {nameof(Contrato.FechaTerm)} = @fechaTerm, 
                    {nameof(Contrato.IdInquilino)} = @idInquilino, 
                    {nameof(Contrato.IdInmueble)} = @idInmueble
                WHERE {nameof(Contrato.Id)} = @id";           
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaInicio)}", contrato.FechaInicio);
                command.Parameters.AddWithValue($"@{nameof(Contrato.FechaTerm)}", contrato.FechaTerm);
                command.Parameters.AddWithValue($"@{nameof(Contrato.IdInquilino)}", contrato.IdInmueble);
                command.Parameters.AddWithValue($"@{nameof(Contrato.IdInmueble)}",contrato.IdInmueble);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                contrato.Id = id;
                connection.Close();


            }
        }
        return 0;
    }

    public int  EliminarContrato(int id){
        using(var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"DELETE from contratos WHERE {nameof(Contrato.Id)} = @{nameof(Contrato.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Contrato.Id)}", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
         return 0;
    }

}