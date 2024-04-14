using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class Pedidos
    {

        [Key]
        public int Id_Pedido { get; set;}
        public int? Id_mesa { get; set;}
        public int? Id_Producto { get; set;}
        public int? Id_EstadoPedido { get; set;}
        public int NumPedido { get; set;}
        public string NombreCliente { get; set;}
        public int CantidadPersonas { get; set;}
        public int Cantidad {  get; set;}
        public string Categoria { get; set;}
        public decimal? Precio { get; set;}
        public decimal Total { get; set;}
        public DateTime? FechaHora { get; set;}
    } 
}
