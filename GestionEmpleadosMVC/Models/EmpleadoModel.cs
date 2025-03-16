﻿using Microsoft.Extensions.Configuration;
using System.Data.SqlTypes;

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
}