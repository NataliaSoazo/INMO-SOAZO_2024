using INMO_SOAZO_2024.Models;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Diagnostics.Metrics.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace INMO_SOAZO_2024.Models;

public class RepositorioPago
{
    readonly string ConnectionString = "Server=localhost;Database=inmosoazo2024;User=root;Password=;";

    public RepositorioPago()
    {

    }

   public IList<Pago> GetAll()
    {
        var pagos = new List<Pago>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"SELECT {nameof(Pago.Id)}, {nameof(Pago.Nro)}, {nameof(Pago.Fecha)}, {nameof(Pago.Referencia)}, {nameof(Pago.Importe)}, {nameof(Pago.Anulado)}, {nameof(Pago.IdContrato)} FROM pagos";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pagos.Add(new Pago
                        {
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
                             Nro = reader.GetInt32(nameof(Pago.Nro)),
                             Fecha = reader.GetDateTime (nameof(Pago.Fecha)),
                             Referencia = reader.GetString(nameof(Pago.Referencia)),
                             Importe = reader.GetDouble (nameof(Pago.Importe)),
                             Anulado = reader.GetString(nameof(Pago.Anulado)),
                             IdContrato = reader.GetInt32(nameof(Pago.IdContrato)),
                             DatosContrato = new Contrato
                             {
                                 Id = reader.GetInt32(nameof(Contrato.Id)),
                                
                             }

                        });
                     }

                }
            }
         }
         return pagos;
    }


    public int AltaDePago(Pago pago){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            var sql = @$"INSERT INTO pagos ({nameof(Pago.Nro)}, {nameof(Pago.Fecha)},
             {nameof(Pago.Referencia)}, {nameof(Pago.Importe)}, {nameof(Pago.Anulado)},
             {nameof(Pago.IdContrato)})
             VALUES (@{nameof(Pago.Nro)}, @{nameof(Pago.Fecha)}, @{nameof(Pago.Referencia)},
             @{nameof(Pago.Importe)}, @{nameof(Pago.Anulado)}, @{nameof(Pago.IdContrato)});
             SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Pago.Nro)}", pago.Nro);
                command.Parameters.AddWithValue($"@{nameof(Pago.Fecha)}", pago.Fecha);
                command.Parameters.AddWithValue($"@{nameof(Pago.Referencia)}",pago.Referencia);
                command.Parameters.AddWithValue($"@{nameof(Pago.Importe)}", pago.Importe);
                command.Parameters.AddWithValue($"@{nameof(Pago.Anulado)}", pago.Anulado);
                command.Parameters.AddWithValue($"@{nameof(Pago.IdContrato)}", pago.IdContrato);
                
                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                pago.Id = id;
                connection.Close();


            }
        }
        return id;
    }

    public Pago? getPago(int id)
    {
        Pago? pago = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            string sql = @$" SELECT {nameof(Pago.Id)}, {nameof(Pago.Nro)}, {nameof(Pago.Fecha)}, {nameof(Pago.Referencia)}, {nameof(Pago.Importe)}, {nameof(Pago.Anulado)}," +
                        @$" c.{nameof(Pago.IdContrato)}, c.{nameof(Pago.DatosContrato)}" +
                        @$" FROM pagos INNER JOIN contratos ON {nameof(Pago.IdContrato)} = {nameof(Contrato.Id)}
               WHERE {nameof(Pago.Id)} = @id;";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inmueble.Id)}", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pago = new Pago
                        {
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
                             Nro = reader.GetInt32(nameof(Pago.Nro)),
                             Fecha = reader.GetDateTime (nameof(Pago.Fecha)),
                             Referencia = reader.GetString(nameof(Pago.Referencia)),
                             Importe = reader.GetDouble (nameof(Pago.Importe)),
                             Anulado = reader.GetString(nameof(Pago.Anulado)),
                             IdContrato = reader.GetInt32(nameof(Pago.IdContrato)),
                             DatosContrato = new Contrato
                             {
                                 Id = reader.GetInt32(nameof(Contrato.Id)),
                                 IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                                 IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                             }
                        };
                    }
                }
            }
            
        }
        return pago;
    }

    public int ModificarPago(Pago pago){
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString)){
            string sql = @$"UPDATE pagos
					SET 
                    {nameof(Pago.Nro)} = @{nameof(Pago.Nro)}, 
                    {nameof(Pago.Fecha)} = @{nameof(Pago.Fecha)}, 
                    {nameof(Pago.Referencia)} = @{nameof(Pago.Referencia)}, 
                    {nameof(Pago.Importe)} = @{nameof(Pago.Importe)}, 
                    {nameof(Pago.Anulado)} = @{nameof(Pago.Anulado)},
                    {nameof(Pago.IdContrato)} = @{nameof(Pago.IdContrato)}, 
                    {nameof(Pago.DatosContrato)} = @{nameof(Pago.DatosContrato)}
					WHERE {nameof(Pago.Id)}= @{nameof(Pago.Id)};";
            using (var command = new MySqlCommand(sql, connection)){
                command.Parameters.AddWithValue($"@{nameof(Pago.Id)}", pago.Id);
                command.Parameters.AddWithValue($"@{nameof(Pago.Nro)}", pago.Nro);
                command.Parameters.AddWithValue($"@{nameof(Pago.Fecha)}", pago.Fecha);
                command.Parameters.AddWithValue($"@{nameof(Pago.Referencia)}",pago.Referencia);
                command.Parameters.AddWithValue($"@{nameof(Pago.Importe)}", pago.Importe);
                command.Parameters.AddWithValue($"@{nameof(Pago.Anulado)}", pago.Anulado);
                command.Parameters.AddWithValue($"@{nameof(Pago.IdContrato)}", pago.IdContrato);
                

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                pago.Id = id;
                connection.Close();


            }
        }
        return 0;
    }

    public int  EliminarPago(int id){
        using(var connection = new MySqlConnection(ConnectionString))
        {
            var sql = @$"DELETE from pagos WHERE {nameof(Pago.Id)} = @{nameof(Pago.Id)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Pago.Id)}", id);
                connection.Open();
               command.ExecuteNonQuery();
               connection.Close();
            }
        }
         return 0;
    }

}