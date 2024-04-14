using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class Productos
    {

        [Key]
        public int Id_Producto { get; set; }
        public int Id_estadoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string categoria {  get; set; }
        public decimal Precio { get; set;}

    }
}
