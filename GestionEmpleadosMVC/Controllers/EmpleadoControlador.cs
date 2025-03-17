// Este controlador maneja las operaciones relacionadas con los empleados en la aplicación.
// Incluye acciones para listar empleados y crear nuevos empleados, interactuando con el modelo EmpleadoModel.

using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GestionEmpleadosMVC.Controllers
{
    public class EmpleadoControlador : Controller
    {
        // Instancia del modelo EmpleadoModel para interactuar con la base de datos.
        private readonly EmpleadoModel empleado = new EmpleadoModel();

        // Acción para listar todos los empleados.
        public IActionResult Index()
        {
            List<Empleado> empleados = empleado.ObtenerEmpleados();
            return View(empleados);
        }

        // Acción para mostrar el formulario de creación de empleados.
        public IActionResult Create()
        {
            return View();
        }

        // Acción para manejar el envío del formulario de creación de empleados.
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
