using System.Net;
using System.Text;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Newtonsoft.Json;

namespace Hackathon.Fiap.Teste.Integracao.Usuarios
{
    [Collection("Hackathon API Collection")]
    public class UsuariosIntegracaoTestes(HackatonApiFactory hackatonApi)
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
        public async Task Listar_Usuarios_Paginas_Corretamente()
        {
            await AutenticarAplicacao(Roles.Administrador);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/usuarios/paginados");
            PaginacaoConsulta<PacienteResponse>? medicosPaginados = JsonConvert.DeserializeObject<PaginacaoConsulta<PacienteResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotNull(medicosPaginados);
            Assert.NotEmpty(medicosPaginados.Registros);
        }

        [Fact]
        public async Task Listar_Usuarios_Paginas_Autorizacao_NaoAutorizado()
        {
            await AutenticarAplicacao(Roles.Paciente);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/usuarios/paginados");

            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Fact]
        public async Task CadastroUsuarioTeste_Bad_Request()
        {
            await AutenticarAplicacao(Roles.Administrador);

            UsuarioCadastroRequest usuarioCadastroRequest = new()
            {

            };

            string jsonContent = JsonConvert.SerializeObject(usuarioCadastroRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiFactoryClient.PostAsync("api/usuarios", httpContent);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CadastroUsuarioTeste_Usuario_Valido_Request()
        {
            await AutenticarAplicacao(Roles.Administrador);

            UsuarioCadastroRequest usuarioCadastroRequest = new()
            {
                Cpf = "215.869.710-50",
                Crm = "1255/SP",
                Email = "email@teste.com",
                Nome = "Teste",
                Senha = "Siosad",
                SobreNome = "Siosa",
                TipoUsuario = TipoUsuario.Medico
            };

            string jsonContent = JsonConvert.SerializeObject(usuarioCadastroRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiFactoryClient.PostAsync("api/usuarios", httpContent);
            UsuarioResponse? response = JsonConvert.DeserializeObject<UsuarioResponse>(await result.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(response.Nome, usuarioCadastroRequest.Nome + " " + usuarioCadastroRequest.SobreNome);
        }
    }
}
