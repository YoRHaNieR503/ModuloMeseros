using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloMeseros.Models;
using System.Linq;

namespace ModuloMeseros.Controllers
{
    public class DetallesController : Controller
    {
        private readonly DulceSavorDbContext _context;

        public DetallesController(DulceSavorDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int idMesaCuenta)
        {
            var listaEstados = (from e in _context.estados
                                join m in _context.mesas on e.id_estado equals m.id_estado
                                join cue in _context.Cuenta on m.id_mesa equals cue.Id_mesa
                                where cue.Id_mesa == idMesaCuenta
                                select Convert.ToInt32(cue.Estado_cuenta)).Take(1).ToList();

            ViewData["Estados"] = listaEstados;

            int idEstado = listaEstados.FirstOrDefault();

            var estado = _context.estados.FirstOrDefault(m => m.id_estado == idEstado);

            if (estado == null)
            {
                return NotFound();
            }

            ViewData["NombreEstado"] = estado.nombre;

            var mesa = _context.mesas.FirstOrDefault(m => m.id_mesa == idMesaCuenta);

            if (mesa == null)
            {
                return NotFound();
            }

            ViewData["IdMesa"] = mesa.id_mesa;
            ViewData["NumeroMesa"] = mesa.nombre_mesa;
            ViewData["CantidadPersonas"] = mesa.cantidad_personas;

            var listadoDetallePedidos = (from dp in _context.Detalle_Pedido
                                         join cue in _context.Cuenta on dp.Id_cuenta equals cue.Id_cuenta
                                         join me in _context.mesas on cue.Id_mesa equals me.id_mesa
                                         join im in _context.items_menu on dp.Id_plato equals im.id_item_menu
                                         where cue.Id_mesa == idMesaCuenta
                                         select new
                                         {
                                             Cantidad = dp.Cantidad,
                                             Pedido = im.nombre,
                                             Precio = dp.Precio,
                                             TipoPlato = dp.Tipo_Plato,
                                             IDCuenta = dp.Id_DetalleCuenta,
                                             IdPlato = dp.Id_plato,
                                             Nombre = cue.Nombre
                                         }).ToList();

            ViewData["ListadoDetallePedido"] = listadoDetallePedidos;

            var nombreCliente = _context.Cuenta.FirstOrDefault(m => m.Id_mesa == idMesaCuenta);

            if (nombreCliente == null)
            {
                return NotFound();
            }

            ViewData["NombreCliente"] = nombreCliente.Nombre;

            var idCuenta = _context.Cuenta.Where(m => m.Id_mesa == idMesaCuenta).Select(c => c.Id_cuenta).FirstOrDefault();

            ViewData["idCuenta"] = idCuenta;

            var totalPrecio = (from dp in _context.Detalle_Pedido
                               join cue in _context.Cuenta on dp.Id_cuenta equals cue.Id_cuenta
                               join me in _context.mesas on cue.Id_mesa equals me.id_mesa
                               join im in _context.items_menu on dp.Id_plato equals im.id_item_menu
                               where cue.Id_mesa == idMesaCuenta
                               select dp.Precio).Sum();
            ViewData["TotalPrecio"] = totalPrecio;

            return View();
        }

       






    }
}
