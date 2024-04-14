using Microsoft.AspNetCore.Mvc;
using ModuloMeseros.Models;

namespace ModuloMeseros.Controllers
{
    public class MesasController : Controller
    {

        private readonly DulceSavorDbContext _DulceSavorDbContext;

        public IActionResult Index()
        {

            var listadoMesas = (from m in _DulceSavorDbContext.Mesas
                                join em in _DulceSavorDbContext.EstadosMesas on m.Id_estado equals em.Id_estadoMesa
                                where em.EstadoMesa == "libre"
                                select new
                                {
                                    Numero_Mesa = m.NumMesa,
                                    Estado = em.EstadoMesa
 
                                }).ToList();
            ViewData["listadoMesa"] = listadoMesas;


            return View();
        }
    }
}
