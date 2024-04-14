using System.ComponentModel.DataAnnotations;

namespace ModuloMeseros.Models
{
    public class Mesas
    {

        [Key]
        public int Id_mesa { get; set; }
        public int? Id_estado { get; set; }
        public string NumMesa { get; set; }

    }
}
