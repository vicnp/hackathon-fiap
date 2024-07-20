using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TC_APP.Models;

namespace TC_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();

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

        [HttpGet]
        public async Task<IActionResult> CriarContato(int id = 0)
        {
            if (id != 0)
            {
                Contato contato = await GetContatoAsync(id);
                return View(contato);
            }

            return View(new Contato());
        }

        [HttpPost]
        public async Task<IActionResult> CrudContato(Contato contato)
        {
            if (contato.Id == null || contato.Id == 0)
                await CriarRegistroContato(contato);
            else
                await EditarRegistroContato(contato);

            return RedirectToAction("Index");
        }

        private async Task CriarRegistroContato(Contato contato)
        {
            var json = JsonConvert.SerializeObject(contato);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:5142/api/contatos";

            var response = await client.PostAsync(url, data);

            await response.Content.ReadAsStringAsync();
        }

        private async Task EditarRegistroContato(Contato contato)
        {
            var json = JsonConvert.SerializeObject(contato);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:5142/api/contatos/{contato.Id}";

            var response = await client.PutAsync(url, data);

            await response.Content.ReadAsStringAsync();
        }

        public async Task<IActionResult> ExcluirRegistroContato(int id)
        {
            var queryString = $"/{id}";

            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:5142/api/contatos{queryString}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(Contato contato)
        {
            List<Contato> lista = await GetContatosAsync(contato);
            return View(lista);
        }

        private async Task<List<Contato>> GetContatosAsync(Contato contato)
        {
            try
            {
                // Converte o objeto Contato em uma string de consulta
                var queryString = $"?nome={contato.Nome}&email={contato.Email}&DDD={contato.DDD}&Regiao={contato.Regiao?.Descricao}"; // Exemplo b�sico, modifique conforme suas propriedades

                HttpResponseMessage response = await client.GetAsync($"https://localhost:5142/api/contatos/itens{queryString}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                List<Contato> dados = System.Text.Json.JsonSerializer.Deserialize<List<Contato>>(responseBody);

                return dados;
            }
            catch
            {
                return new List<Contato>();
            }
        }

        private async Task<Contato> GetContatoAsync(int id)
        {
            try
            {
                // Converte o objeto Contato em uma string de consulta
                var queryString = $"/{id}";

                HttpResponseMessage response = await client.GetAsync($"https://localhost:5142/api/contatos{queryString}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Contato dado = System.Text.Json.JsonSerializer.Deserialize<Contato>(responseBody);

                return dado;
            }
            catch
            {
                return null;
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
