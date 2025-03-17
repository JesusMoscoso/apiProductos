using Microsoft.EntityFrameworkCore;
using ProductosApi.Models;

namespace ProductosApi.Data
{
    public class ApiDbContext:DbContext
    {
        public ApiDbContext(DbContextOptions options):base(options) { }

        public DbSet<Proveedor>Proveedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>()
                .Property(v => v.Estado)
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Venta>()
                .HasCheckConstraint("CHK_Venta_Estado", "Estado IN ('Pendiente', 'Cancelada')");
        }

    }


}
