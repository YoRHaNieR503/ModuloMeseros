using Microsoft.EntityFrameworkCore;

namespace ModuloMeseros.Models
{
    public class DulceSavorDbContext:DbContext
    {

        public DulceSavorDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<EstadoDeProductos> EstadoDeProductos { get; set; }
        public DbSet<EstadoPedido> EstadoPedido { get; set; }
        public DbSet<EstadosMesas> EstadosMesas { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Productos> Productos { get; set; }

    }
}
