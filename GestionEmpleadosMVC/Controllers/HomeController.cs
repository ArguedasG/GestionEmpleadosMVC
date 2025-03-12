using System.Data.SqlClient;
using System.Diagnostics;
using GestionEmpleadosMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpleadosMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly String ConnectionString;

        public HomeController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConnectionString))
                {
                    cnn.Open();
                    ViewBag.Mensaje = "Conexión exitosa!";
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al conectar: " + ex.Message;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
