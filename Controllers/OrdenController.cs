using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModuloMeseros.Models;

namespace ModuloMeseros.Controllers
{
    public class OrdenController : Controller
    {
        private readonly DulceSavorDbContext _DulceSavorDbContext;

        public OrdenController(DulceSavorDbContext DulceSavorDbContexto)
        {
            _DulceSavorDbContext = DulceSavorDbContexto;
        }



        public IActionResult Index(int idMesa)
        {
            DateTime hoy = DateTime.Today;

            int cantidadOrdenesHoy = _DulceSavorDbContext.Cuenta.Count(c => c.Fecha_Hora.Date == hoy) + 1;

            ViewData["ListaCuentassCount"] = cantidadOrdenesHoy;

            var listaEstados = (from e in _DulceSavorDbContext.estados
                                where e.tipo_estado == "Orden"
                                select e).ToList();

            ViewData["Estados"] = new SelectList(listaEstados, "id_estado", "nombre");


            var mesa = _DulceSavorDbContext.mesas.FirstOrDefault(m => m.id_mesa == idMesa);

            if (mesa == null)
            {
                // Si la mesa no se encuentra en la base de datos.
                return NotFound(); // Devuelve una respuesta HTTP 404 - Not Found
            }

            // Pasar los detalles de la mesa a la vista
            ViewData["NumeroMesa"] = mesa.nombre_mesa;
            ViewData["CantidadPersonas"] = mesa.cantidad_personas;

            return View();
        }
    }
}
