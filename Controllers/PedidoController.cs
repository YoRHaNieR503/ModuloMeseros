using Microsoft.AspNetCore.Mvc;
using ModuloMeseros.Models;

namespace ModuloMeseros.Controllers
{
    public class PedidoController : Controller
    {

        private readonly DulceSavorDbContext _context;

        public PedidoController(DulceSavorDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(int id_categoria, int idMesa)
        {

            var listaProductos = (from ca in _context.categorias
                                  join im in _context.items_menu on ca.id_categoria equals im.id_categoria
                                  join e in _context.estados on im.id_estado equals e.id_estado
                                  where im.id_categoria == id_categoria
                                  && e.tipo_estado == "disponibilidad"
                                  select new
                                  {
                                      Producto = im.nombre,
                                      Estado = e.nombre,
                                      Precio = im.precio
                                  }).ToList();
            ViewData["listaProductos"] = listaProductos;


            var categoria = _context.categorias.FirstOrDefault(m => m.id_categoria == id_categoria);

            if (categoria == null)
            {
                return NotFound(); // Devuelve una respuesta HTTP 404 - Not Found
            }

            ViewData["Categoria"] = categoria.categoria;

            var id_Mesa = _context.Cuenta.FirstOrDefault(m => m.Id_mesa == idMesa);

            if (id_Mesa == null)
            {
                return NotFound(); // Devuelve una respuesta HTTP 404 - Not Found
            }

            ViewData["idMesa"] = id_Mesa;

            var id_cuenta = (from c in _context.Cuenta
                             where c.Id_mesa == idMesa
                             select c.Id_cuenta).Take(1).ToList();

            ViewData["idCuenta"] = id_cuenta;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CrearCuenta(int idMesa, string nombre, int cantPersonas, string estadoCuenta, DateTime fechaHora)
        {
            var nuevaCuenta = new Cuenta
            {
                Id_mesa = idMesa,
                Nombre = nombre,
                Cantidad_Personas = cantPersonas,
                Estado_cuenta = estadoCuenta,
                Fecha_Hora = fechaHora
            };

            _DulceSavorDbContext.Cuenta.Add(nuevaCuenta);
            await _DulceSavorDbContext.SaveChangesAsync();

            // Llama al método para actualizar el estado de la mesa a "Ocupado" (ID = 1)
            await ActualizarEstadoMesa(idMesa, 1);


            // Vuelve a cargar la vista actual con los datos actualizados
            return RedirectToAction("Index", new { idMesa });
        }



    }
}
