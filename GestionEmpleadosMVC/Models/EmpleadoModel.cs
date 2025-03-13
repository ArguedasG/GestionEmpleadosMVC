using System.Data.SqlTypes;

namespace GestionEmpleadosMVC.Models
{
    public class EmpleadoModel
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public SqlMoney Salario { get; set; }
    }
}
