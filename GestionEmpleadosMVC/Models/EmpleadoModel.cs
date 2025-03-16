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
    private readonly string connectionString;

    public EmpleadoModel(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

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

            SqlParameter = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(Resultado);

            cmd.connection = connection;
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        return (int)cmd.Parameters["@Resultado"].Value;
    }

}