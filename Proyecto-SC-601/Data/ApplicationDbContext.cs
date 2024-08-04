using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Models;

namespace Proyecto_SC_601.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<DireccionEnvio> DireccionesEnvio { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.IDUsuario);
            modelBuilder.Entity<Categoria>().HasKey(ct => ct.IDCategoria);
            modelBuilder.Entity<Proveedor>().HasKey(pv => pv.IDProveedor);
            modelBuilder.Entity<Producto>().HasKey(pd => pd.IDProducto);
            modelBuilder.Entity<Carrito>().HasKey(cr => cr.IDDetalle);
            modelBuilder.Entity<DireccionEnvio>().HasKey(de => de.IDDireccion);
            modelBuilder.Entity<Pedido>().HasKey(p => p.IDPedido);
            modelBuilder.Entity<DetallePedido>().HasKey(dp => dp.IDDetalle);
        }
    }
}
