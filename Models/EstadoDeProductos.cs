using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class EstadoDeProductos
    {

        [Key]
        public int Id_estadoProducto { get; set; }
        public string? EstadoProductos { get; set; }


    }
}
