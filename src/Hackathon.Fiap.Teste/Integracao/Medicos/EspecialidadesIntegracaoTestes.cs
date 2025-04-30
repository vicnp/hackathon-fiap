using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Hackathon.Fiap.Teste.Integracao.Medicos
{
    [Collection("Hackathon API Collection")]
    public class EspecialidadesIntegracaoTestes(HackatonApiFactory hackatonApi)
    {

        private readonly HttpClient apiFactoryClient = hackatonApi.CreateClient();
        private string tokenJwt = string.Empty;

        private async Task AutenticarAplicacao(string role)
        {
            string usuario = $"SIS@{role.ToUpper()}.com";
            string senha = "123";
            HttpResponseMessage result = await apiFactoryClient.PostAsync($"api/auth?identificador={usuario}&senha={senha}", null);
            Assert.True(result.IsSuccessStatusCode, "Autenticação falhou.");
            tokenJwt = await result.Content.ReadAsStringAsync();
            apiFactoryClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenJwt);
        }

        [Fact]
        public async Task CadastroEspecialidadeValidoRequest()
        {
            await AutenticarAplicacao(Roles.Administrador);

            EspecialidadeRequest especialidadeRequest = new()
            {
                NomeEspecialidade = "Teste",
                DescricaoEspecialidade = "Siosad"
            };

            string jsonContent = JsonConvert.SerializeObject(especialidadeRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiFactoryClient.PostAsync("api/especialidades", httpContent);
            UsuarioResponse? response = JsonConvert.DeserializeObject<UsuarioResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Listar_Especialidades_Paginas_Corretamente()
        {
            await AutenticarAplicacao(Roles.Administrador);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/especialidades/paginados");
            PaginacaoConsulta<EspecialidadeResponse>? especialidadesPaginados = JsonConvert.DeserializeObject<PaginacaoConsulta<EspecialidadeResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotNull(especialidadesPaginados);
            Assert.NotEmpty(especialidadesPaginados.Registros);
        }
    }
}
