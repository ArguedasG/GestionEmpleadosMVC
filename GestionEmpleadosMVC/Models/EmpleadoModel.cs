using System.Data.SqlTypes;

namespace GestionEmpleadosMVC.Models
{
    public class Empleado
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public SqlMoney Salario { get; set; }
    }
}
