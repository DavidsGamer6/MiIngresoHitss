using Microsoft.EntityFrameworkCore;
using MiIngresoHitss.Entities;
using System.Collections.Generic;

namespace MiIngresoHitss.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ListaDePrecios> ListasDePrecios { get; set; }
        public DbSet<ProductoListaPrecio> ProductoListaPrecios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVentas { get; set; }
    }
}
