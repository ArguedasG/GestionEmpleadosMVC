// Este archivo define dos clases: Empleado y EmpleadoModel.
// La clase Empleado representa un empleado con propiedades como id, Nombre y Salario.
// La clase EmpleadoModel maneja la lógica de acceso a datos para obtener e insertar empleados en la base de datos.
// Utiliza stored procedures para realizar estas operaciones.

using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;

public class Empleado
{
    public int id { get; set; }
    public string Nombre { get; set; }
    public decimal Salario { get; set; }
}

public class EmpleadoModel
{
    // Cadena de conexión a la base de datos
    private readonly string connectionString = "Server=mssql-193245-0.cloudclusters.net,10068;Database=BaseNumero1;User Id=Daniel;Password=Daniel300924;TrustServerCertificate=True;";

    // Método para obtener la lista de empleados desde la base de datos
    public List<Empleado> ObtenerEmpleados() 
    {
        List<Empleado> empleados = new List<Empleado>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Configuración del comando para ejecutar el stored procedure
            SqlCommand command = new SqlCommand("sp_GetEmpleados", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Lectura de los datos obtenidos y creación de objetos de tipo Empleado
            while (reader.Read())
            {
                empleados.Add(new Empleado
                {
                    id = Convert.ToInt32(reader["id"]),
                    Nombre = reader["Nombre"].ToString(),
                    Salario = Convert.ToDecimal(reader["Salario"])
                });
            }
        }
         
        return empleados;
    }

    // Método para insertar un nuevo empleado en la base de datos
    public int InsertarEmpleado(string nombre, SqlMoney salario)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("sp_InsertEmpleado", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Adición de parámetros
            command.Parameters.AddWithValue("@Nombre", nombre);
            command.Parameters.AddWithValue("@Salario", (SqlMoney)salario);

            // Parámetro de salida para obtener el resultado de la inserción
            SqlParameter Resultado = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(Resultado);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)Resultado.Value;
        }
         
    }

}