using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hackathon.Fiap.Teste.Integracao.Medicos
{
    [Collection("Hackathon API Collection")]
    public class MedicosIntegracaoTestes(HackatonApiFactory hackatonApi)
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
        public async Task Listar_Medicos_Paginas_Corretamente()
        {
            await AutenticarAplicacao(Roles.Medico);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/medicos/paginados");
            PaginacaoConsulta<MedicoResponse>? medicosPaginados = JsonConvert.DeserializeObject<PaginacaoConsulta<MedicoResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotNull(medicosPaginados);
            Assert.NotEmpty(medicosPaginados.Registros);
        }

        [Fact]
        public async Task Listar_Medicos_Paginas_Autorizacao_NaoAutorizado()
        {
            await AutenticarAplicacao(Roles.Administrador);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/medicos/paginados");

            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }
    }
}
