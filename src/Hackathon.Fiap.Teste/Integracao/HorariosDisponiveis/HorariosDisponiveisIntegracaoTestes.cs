using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Hackathon.Fiap.Teste.Integracao.HorariosDisponiveis
{
    [Collection("Hackathon API Collection")]
    public class HorariosDisponiveisIntegracaoTestes(HackatonApiFactory hackatonApi)
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
        public async Task Listar_Consultas_Paginas_Corretamente()
        {
            await AutenticarAplicacao(Roles.Medico);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/horarios-disponiveis/paginados");
            PaginacaoConsulta<HorarioDisponivelResponse>? consultasPaginadas = JsonConvert.DeserializeObject<PaginacaoConsulta<HorarioDisponivelResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotNull(consultasPaginadas);
            Assert.NotEmpty(consultasPaginadas.Registros);
        }

        [Fact]
        public async Task Inserir_Registro_HorarioDisponivel_Corretamente()
        {
            await AutenticarAplicacao(Roles.Medico);

            HorarioDisponivelInserirRequest horarioDisponivelInserirRequest = new()
            {
                DataHoraFim = DateTime.Now.AddDays(1),
                DataHoraInicio = DateTime.Now,
                IdMedico = 1
            };

            string jsonContent = JsonConvert.SerializeObject(horarioDisponivelInserirRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiFactoryClient.PostAsync("api/horarios-disponiveis/inserir", httpContent);
            Assert.True(result.StatusCode == HttpStatusCode.Created);
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.ReasonPhrase == "Created");
        }
    }
}
