﻿using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;

public class Empleado
{
    public int id { get; set; }
    public string Nombre { get; set; }
    public SqlMoney Salario { get; set; }
}

public class EmpleadoModel
{
    private readonly string connectionString = "Server=mssql-193245-0.cloudclusters.net,10068;Database=BaseNumero1;User Id=Daniel;Password=Daniel300924;TrustServerCertificate=True;";

    public List<Empleado> ObtenerEmpleados() 
    {
        List<Empleado> empleados = new List<Empleado>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("sp_GetEmpleados", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                empleados.Add(new Empleado
                {
                    id = Convert.ToInt32(reader["id"]),
                    Nombre = reader["Nombre"].ToString(),
                    Salario = (SqlMoney)reader["Salario"]
                });
            }
        }
         
        return empleados;
    }

    public int InsertarEmpleado(string nombre, SqlMoney salario)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("sp_InsertEmpleado", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombre", nombre);
            command.Parameters.AddWithValue("@Salario", salario);

            SqlParameter Resultado = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(Resultado);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)Resultado.Value;
        }
         
    }

}