﻿using System.Net;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;
using Newtonsoft.Json;

namespace Hackathon.Fiap.Teste.Integracao.Pacientes
{
    [Collection("Hackathon API Collection")]
    public class PacientesIntegracaoTestes(HackatonApiFactory hackatonApi)
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
        public async Task Listar_Pacientes_Paginas_Corretamente()
        {
            await AutenticarAplicacao(Roles.Medico);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/pacientes/paginados");
            PaginacaoConsulta<PacienteResponse>? medicosPaginados = JsonConvert.DeserializeObject<PaginacaoConsulta<PacienteResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotNull(medicosPaginados);
            Assert.NotEmpty(medicosPaginados.Registros);
        }

        [Fact]
        public async Task Listar_Pacientes_Paginas_Autorizacao_NaoAutorizado()
        {
            await AutenticarAplicacao(Roles.Paciente);
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/pacientes/paginados");

            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }
    }
}
