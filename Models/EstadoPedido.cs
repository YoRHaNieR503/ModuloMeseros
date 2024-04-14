using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class EstadoPedido
    {

        [Key]
        public int Id_EstadoPedido { get; set; }
        public string EstadoPedidos { get; set; }

    }
}
