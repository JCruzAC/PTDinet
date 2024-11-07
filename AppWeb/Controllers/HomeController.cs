using AppWeb.Models;
using CapaEntidades;
using CapaNegocio.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovimientoInventarioService _movimientoInventarioService;

        public HomeController(ILogger<HomeController> logger
            , IMovimientoInventarioService movimientoInventarioService)
        {
            _logger = logger;
            _movimientoInventarioService = movimientoInventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? fechaInicio, string? fechaFin, string tipoMovimiento, string numeroDocumento)
        {
            DateTime? fechaInicioParsed=null, fechaFinParsed=null;

            if (!string.IsNullOrEmpty(fechaInicio) && DateTime.TryParse(fechaInicio, out DateTime parsedFechaInicio))
            {
                fechaInicioParsed = parsedFechaInicio;
            }

            if (!string.IsNullOrEmpty(fechaFin) && DateTime.TryParse(fechaFin, out DateTime parsedFechaFin))
            {
                fechaFinParsed = parsedFechaFin;
            }
            List<MovimientoInventario> lista = await _movimientoInventarioService
                .ListadoMovimientoInventario( fechaInicioParsed,fechaFinParsed, tipoMovimiento, numeroDocumento);
            TempData["Mensaje"] = _movimientoInventarioService._message;
            ViewBag.fechaInicio = fechaInicio;//?.ToString("yyyy-MM-dd");
            ViewBag.fechaFin = fechaFin;//?.ToString("yyyy-MM-dd");
            ViewBag.tipoMovimiento = tipoMovimiento;
            ViewBag.numeroDocumento = numeroDocumento;
            return View(lista);
        }
        [HttpGet]
        public async Task<IActionResult> NuevoMovimientoInventario()
        {
            MovimientoInventario model = new();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NuevoMovimientoInventario([FromForm]MovimientoInventario model)
        {
            model.FECHA_TRANSACCION = DateTime.Now;
            if (ModelState.IsValid)
            {
                bool respuesta = await _movimientoInventarioService.CrearMovimientoInventario(model);
                TempData["Mensaje"] = _movimientoInventarioService._message;
                if (respuesta)
                    return RedirectToAction("index");
            }
            else
                TempData["Mensaje"] = "Datos no válidos";

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
