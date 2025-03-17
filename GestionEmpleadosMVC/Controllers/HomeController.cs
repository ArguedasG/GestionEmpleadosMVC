using System.Data.SqlClient;
using System.Diagnostics;
using GestionEmpleadosMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpleadosMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "EmpleadoControlador");
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
