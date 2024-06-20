using MiIngresoHitss.Data;
using MiIngresoHitss.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MiIngresoHitss.Business
{
    public class ProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Producto> GetAllProductos()
        {
            return _context.Productos.ToList();
        }

        public Producto GetProductoById(int id)
        {
            return _context.Productos.Find(id);
        }

        public void AddProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        public void UpdateProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            _context.SaveChanges();
        }

        public void DeleteProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
        }
    }
}
