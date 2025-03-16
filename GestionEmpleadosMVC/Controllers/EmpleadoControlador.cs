using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GestionEmpleadosMVC.Controllers
{
    public class EmpleadoControlador : Controller
    {
        private readonly EmpleadoModel empleado;

        private readonly string connectionString;

        public EmpleadoControlador(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = empleado.ObtenerEmpleados();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
