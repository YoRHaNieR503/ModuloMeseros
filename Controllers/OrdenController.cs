using Microsoft.AspNetCore.Mvc;
using ModuloMeseros.Models;

namespace ModuloMeseros.Controllers
{
    public class OrdenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
