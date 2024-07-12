using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TC_APP.Models;

namespace TC_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Contato> lista = new List<Contato> { new Contato
            {
                DDD = 27,
                Email = "vitin@asd.com",
                Id = 1,
                Nome = "Vitin do RJ",
                Telefone = "+55 (27) 99881-1224"
            } };
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
