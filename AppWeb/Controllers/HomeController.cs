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

        public async Task<IActionResult> Index()
        {
            List<MovimientoInventario> lista = await _movimientoInventarioService.ListadoMovimientoInventario(null,null,null,null);
            TempData["Mensaje"] = _movimientoInventarioService._message;
            return View(lista);
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
