using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class Comentarios
    {

        [Key]
        public int Id_comentario { get; set; }
        public int? Id_producto { get; set; }
        public int? Id_Pedido { get;}
        public string Comentario { get; set; }

    }
}
