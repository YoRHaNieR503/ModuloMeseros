using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModuloMeseros.Models;

namespace ModuloMeseros.Controllers
{
    public class DetallesController : Controller
    {
        private readonly DulceSavorDbContext _DulceSavorDbContext;

        public DetallesController(DulceSavorDbContext DulceSavorDbContexto)
        {
            _DulceSavorDbContext = DulceSavorDbContexto;
        }

        public IActionResult Index(int idMesaCuenta)
        {


            var listaEstados = (from e in _DulceSavorDbContext.estados
                                join m in _DulceSavorDbContext.mesas on e.id_estado equals m.id_estado
                                join cue in _DulceSavorDbContext.Cuenta on m.id_mesa equals cue.Id_mesa
                                where cue.Id_mesa == idMesaCuenta
                                select Convert.ToInt32(cue.Estado_cuenta)).Take(1).ToList();

            ViewData["Estados"] = listaEstados;


            int idestado = ((List<int>)ViewData["Estados"])[0];

            var estados = _DulceSavorDbContext.estados.FirstOrDefault(m => m.id_estado == idestado);

            if (estados == null)
            {
                // Si la mesa no se encuentra en la base de datos.
                return NotFound(); // Devuelve una respuesta HTTP 404 - Not Found
            }

            // Pasar los detalles de la mesa a la vista
            ViewData["NombreEstado"] = estados.nombre;

            var mesa = _DulceSavorDbContext.mesas.FirstOrDefault(m => m.id_mesa == idMesaCuenta);

            if (mesa == null)
            {
                // Si la mesa no se encuentra en la base de datos.
                return NotFound(); // Devuelve una respuesta HTTP 404 - Not Found
            }

            // Pasar los detalles de la mesa a la vista
            ViewData["IdMesa"] = mesa.id_mesa;
            ViewData["NumeroMesa"] = mesa.nombre_mesa;
            ViewData["CantidadPersonas"] = mesa.cantidad_personas;



            var listadoDetallePedidos = (from dp in _DulceSavorDbContext.Detalle_Pedido
                                         join cue in _DulceSavorDbContext.Cuenta on dp.Id_cuenta equals cue.Id_cuenta
                                         join me in _DulceSavorDbContext.mesas on cue.Id_mesa equals me.id_mesa
                                         join im in _DulceSavorDbContext.items_menu on dp.Id_plato equals im.id_item_menu
                                         where cue.Id_mesa == idMesaCuenta
                                         select new
                                         {
                                             Cantidad = dp.Cantidad,
                                             Pedido = im.nombre,
                                             Precio = dp.Precio,
                                             TipoPlato = dp.Tipo_Plato,
                                             iDCuenta = dp.Id_DetalleCuenta,
                                             idPlato = dp.Id_plato,
                                             Nombre = cue.Nombre
                                         }).ToList();

            ViewData["listadoDetallePedido"] = listadoDetallePedidos;


            var nombre = _DulceSavorDbContext.Cuenta.FirstOrDefault(m => m.Id_mesa == idMesaCuenta);

            if (nombre == null)
            {
                // Si la mesa no se encuentra en la base de datos.
                return NotFound(); // Devuelve una respuesta HTTP 404 - Not Found
            }

            // Pasar los detalles de la mesa a la vista
            ViewData["NombreCliente"] = nombre.Nombre;

            var totalPrecio = (from dp in _DulceSavorDbContext.Detalle_Pedido
                               join cue in _DulceSavorDbContext.Cuenta on dp.Id_cuenta equals cue.Id_cuenta
                               join me in _DulceSavorDbContext.mesas on cue.Id_mesa equals me.id_mesa
                               join im in _DulceSavorDbContext.items_menu on dp.Id_plato equals im.id_item_menu
                               where cue.Id_mesa == idMesaCuenta
                               select dp.Precio).Sum();
            ViewData["TotalPrecio"] = totalPrecio;



            return View();
        }
    }
}
