using Microsoft.AspNetCore.Mvc;
using MiIngresoHitss.Business;
using MiIngresoHitss.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MiIngresoHitss.Presentation.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            var productos = _productoService.GetAllProductos();
            return (IActionResult)View(productos);
        }

        public IActionResult Details(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(producto);
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(int productoId)
        {
            var producto = _productoService.GetProductoById(productoId);
            if (producto == null)
            {
                return NotFound();
            }

            var carrito = ObtenerCarritoDeCompras();
            var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);

            if (item == null)
            {
                carrito.Add(new CarritoItem
                {
                    ProductoId = producto.ProductoId,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = 1
                });
            }
            else
            {
                item.Cantidad++;
            }

            GuardarCarritoDeCompras(carrito);
            return RedirectToAction("Index");
        }

        public IActionResult VerCarrito()
        {
            var carrito = ObtenerCarritoDeCompras();
            return View(carrito);
        }

        [HttpPost]
        public IActionResult FinalizarCompra()
        {
            var carrito = ObtenerCarritoDeCompras();
            if (carrito.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // Aquí puedes implementar la lógica para guardar la compra en la base de datos.
            // Por simplicidad, aquí solo vamos a limpiar el carrito.
            LimpiarCarritoDeCompras();

            return RedirectToAction("CompraFinalizada");
        }

        public IActionResult CompraFinalizada()
        {
            return View();
        }

        private List<CarritoItem> ObtenerCarritoDeCompras()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("CarritoDeCompras");
            if (carrito == null)
            {
                carrito = new List<CarritoItem>();
            }
            return carrito;
        }

        private void GuardarCarritoDeCompras(List<CarritoItem> carrito)
        {
            HttpContext.Session.SetObjectAsJson("CarritoDeCompras", carrito);
        }

        private void LimpiarCarritoDeCompras()
        {
            HttpContext.Session.Remove("CarritoDeCompras");
        }


        public IActionResult Create()
        {
            return (IActionResult)View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoService.AddProducto(producto);
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            return (IActionResult)View(producto);
        }

        public IActionResult Edit(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return (IActionResult)BadRequest();
            }

            if (ModelState.IsValid)
            {
                _productoService.UpdateProducto(producto);
                return (IActionResult)RedirectToAction(nameof(Index));
            }
            return (IActionResult)View(producto);
        }

        public IActionResult Delete(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
            {
                return (IActionResult)HttpNotFound();
            }
            return (IActionResult)View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productoService.DeleteProducto(id);
            return (IActionResult)RedirectToAction(nameof(Index));
        }
    }
}
