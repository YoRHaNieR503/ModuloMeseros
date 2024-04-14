using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class EstadosMesas
    {
        [Key]
        public int Id_estadoMesa { get; set; }
        public string? EstadoMesa { get; set; }

    }
}
