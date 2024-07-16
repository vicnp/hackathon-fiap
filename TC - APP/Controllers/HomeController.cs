using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text.Json;
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

        [HttpGet]
        public IActionResult Index()
        {
            List<Contato> lista = GetContatosAsync(new Contato()).Result;
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Contato contato)
        {
            List<Contato> lista = await GetContatosAsync(contato);
            return View(lista);
        }

        private async Task<List<Contato>> GetContatosAsync(Contato contato)
        {
            using HttpClient client = new HttpClient();

            try
            {
                // Converte o objeto Contato em uma string de consulta
                var queryString = $"?nome={contato.Nome}&email={contato.Email}&DDD={contato.DDD}&Regiao={contato.Regiao?.Descricao}"; // Exemplo básico, modifique conforme suas propriedades

                HttpResponseMessage response = await client.GetAsync($"https://localhost:7192/api/contatos/itens{queryString}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                List<Contato> dados =  JsonSerializer.Deserialize<List<Contato>>(responseBody);

                return dados;
            }
            catch
            {
                return new List<Contato>();
            }
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
