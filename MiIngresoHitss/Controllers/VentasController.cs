using Microsoft.AspNetCore.Mvc;
using MiIngresoHitss.Business;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MiIngresoHitss.Presentation.Controllers
{
    public class VentasController : Controller
    {
        private readonly VentaService _ventaService;

        public VentasController(VentaService ventaService)
        {
            _ventaService = ventaService;
        }

        public IActionResult Reporte()
        {
            return (IActionResult)View();
        }

        [HttpPost]
        public async Task<IActionResult> Reporte(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = await _ventaService.ObtenerVentasPorFecha(fechaInicio, fechaFin);
            return View(ventas);
        }
    }
}

