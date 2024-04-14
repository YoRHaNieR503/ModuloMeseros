using Microsoft.AspNetCore.Mvc;
using ModuloMeseros.Models;

namespace ModuloMeseros.Controllers
{
    public class MesasController : Controller
    {

        private readonly DulceSavorDbContext _DulceSavorDbContext;

        public MesasController(DulceSavorDbContext DulceSavorDbContexto)
        {
            _DulceSavorDbContext = DulceSavorDbContexto;
        }

        public IActionResult Index()
        {

            int cantidadMesasOcupadas = (from m in _DulceSavorDbContext.Mesas
                                       join em in _DulceSavorDbContext.EstadosMesas on m.Id_estado equals em.Id_estadoMesa
                                       where em.EstadoMesa == "ocupada"
                                       select m
                                       ).Count();

            ViewData["ListaMesasOcupadas"] = cantidadMesasOcupadas;

            int cantidadMesasLibres = (from m in _DulceSavorDbContext.Mesas
                                       select m
                                       ).Count();

            ViewData["ListaMesasCount"]=cantidadMesasLibres;

            var listadoMesas = (from m in _DulceSavorDbContext.Mesas
                                join em in _DulceSavorDbContext.EstadosMesas on m.Id_estado equals em.Id_estadoMesa
                                where em.EstadoMesa == "libre"
                                select new
                                {
                                    Numero_Mesa = m.NumMesa,
                                    Estado = em.EstadoMesa
 
                                }).ToList();
            ViewData["listadoMesa"] = listadoMesas;

            var listadoMesasTodas = (from m in _DulceSavorDbContext.Mesas
                                join em in _DulceSavorDbContext.EstadosMesas on m.Id_estado equals em.Id_estadoMesa
                                select new
                                {
                                    Numero_Mesa = m.NumMesa,
                                    Estado = em.EstadoMesa

                                }).ToList();
            ViewData["listadoMesaTodas"] = listadoMesasTodas;


            return View();
        }
    }
}
