using Microsoft.AspNetCore.Mvc;
using MiIngresoHitss.Business;
using MiIngresoHitss.Entities;
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
