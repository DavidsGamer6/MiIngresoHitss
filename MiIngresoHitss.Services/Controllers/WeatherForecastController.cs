using Microsoft.AspNetCore.Mvc;
using MiIngresoHitss.Business;
using MiIngresoHitss.Entities;
using System.Collections.Generic;

namespace MiIngresoHitss.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            return Ok(_productoService.GetAllProductos());
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public ActionResult<Producto> PostProducto(Producto producto)
        {
            _productoService.AddProducto(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, producto);
        }

        [HttpPut("{id}")]
        public IActionResult PutProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest();
            }
            _productoService.UpdateProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            _productoService.DeleteProducto(id);
            return NoContent();
        }
    }
}

