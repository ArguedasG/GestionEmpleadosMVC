using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GestionEmpleadosMVC.Controllers
{
    public class EmpleadoControlador : Controller
    {
        private readonly EmpleadoModel empleado = new EmpleadoModel();

        public IActionResult Index()
        {
            List<Empleado> empleados = empleado.ObtenerEmpleados();
            return View(empleados);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Nombre, decimal Salario)
        {
            int Resultado = empleado.InsertarEmpleado(Nombre, Salario);

            if (Resultado == 50001) {
                ViewBag.Error = "El nombre ya esta en uso";
            }
            else { 
                ViewBag.Message = "Empleado creado con exito";
            }

            return View();
        }
    }
}
