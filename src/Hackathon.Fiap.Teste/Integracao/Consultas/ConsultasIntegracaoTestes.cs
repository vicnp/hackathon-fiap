﻿using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Teste.Integracao.ClassesHelper;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Hackathon.Fiap.Teste.Integracao.Consultas
{
    [Collection("Hackathon API Collection")]
    public class ConsultasIntegracaoTestes(HackatonApiFactory hackatonApi)
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
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/consultas/paginados");
            PaginacaoConsulta<ConsultaResponse>? consultasPaginadas = JsonConvert.DeserializeObject<PaginacaoConsulta<ConsultaResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotNull(consultasPaginadas);
            Assert.NotEmpty(consultasPaginadas.Registros);
        }

        [Fact]
        public async Task Listar_Consultas_Autorizacao_ErroSemAutorizacao()
        {
            await AutenticarAplicacao(Roles.Administrador);

            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/consultas/paginados");

            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Fact]
        public async Task AlterarSituacaoConsulta_Autorizacao_SemAutorizacao()
        {
            await AutenticarAplicacao(Roles.Administrador);
            HttpResponseMessage result = await apiFactoryClient.PutAsync("api/consultas/situacoes", null);
            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Fact]
        public async Task AlterarSituacaoConsulta_Status_Invalido_Espero_Erro()
        {
            await AutenticarAplicacao(Roles.Medico);

            HttpResponseMessage resultConsulta = await apiFactoryClient.GetAsync("api/consultas/paginados");
            PaginacaoConsulta<ConsultaResponse>? consultasPaginadas =
                JsonConvert.DeserializeObject<PaginacaoConsulta<ConsultaResponse>>(await resultConsulta.Content.ReadAsStringAsync());

            Assert.NotNull(consultasPaginadas);
            Assert.NotEmpty(consultasPaginadas.Registros);

            ConsultaResponse consultaResponse = consultasPaginadas.Registros.Where(x => x.Status == "Cancelada").First();

            Assert.NotNull(consultaResponse);

            ConsultaStatusRequest consultaStatusRequest = new()
            {
                ConsultaId = consultaResponse.ConsultaId,
                Justificativa = "Justificativa",
                Status = StatusConsultaEnum.Aceita
            };

            string jsonContent = JsonConvert.SerializeObject(consultaStatusRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            HttpResponseMessage resultAlteracao =
                await apiFactoryClient.PutAsync($"api/consultas/situacoes?ConsultaId={consultaResponse.ConsultaId}&Status=Aceita", httpContent);

            ErroResponse? consultaAlterada = JsonConvert.DeserializeObject<ErroResponse>(await resultAlteracao.Content.ReadAsStringAsync());

            Assert.NotNull(consultaAlterada);
            Assert.Equal(Convert.ToInt32(HttpStatusCode.BadRequest), consultaAlterada.Erro.StatusCode);
            Assert.Equal("A consulta está cancelada.", consultaAlterada.Erro.Mensagem);
            Assert.Equal("RegraDeNegocioExcecao", consultaAlterada.Erro.Tipo);
        }

        [Fact]
        public async Task AlterarSituacaoConsulta_Status_Valido_Espero_Consulta_Aceita()
        {
            await AutenticarAplicacao(Roles.Medico);

            HttpResponseMessage resultConsulta = await apiFactoryClient.GetAsync("api/consultas/paginados");
            PaginacaoConsulta<ConsultaResponse>? consultasPaginadas = JsonConvert.DeserializeObject<PaginacaoConsulta<ConsultaResponse>>(await resultConsulta.Content.ReadAsStringAsync());

            Assert.NotNull(consultasPaginadas);
            Assert.NotEmpty(consultasPaginadas.Registros);

            ConsultaResponse consultaResponse = consultasPaginadas.Registros.Where(x => x.Status == "Pendente").First();

            Assert.NotNull(consultaResponse);

            ConsultaStatusRequest consultaStatusRequest = new()
            {
                ConsultaId = consultaResponse.ConsultaId,
                Justificativa = "Justificativa",
                Status = StatusConsultaEnum.Aceita
            };

            string jsonContent = JsonConvert.SerializeObject(consultaStatusRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage resultAlteracao =
                await apiFactoryClient.PutAsync($"api/consultas/situacoes?ConsultaId={consultaResponse.ConsultaId}&Status=Aceita", httpContent);

            Assert.True(resultAlteracao.IsSuccessStatusCode, "O endpoint não respondeu como esperado.");

            if (resultAlteracao.IsSuccessStatusCode)
            {
                ConsultaResponse? consultaAlterada = JsonConvert.DeserializeObject<ConsultaResponse>(await resultAlteracao.Content.ReadAsStringAsync());
                Assert.NotNull(consultaAlterada);
                Assert.Equal("Aceita", consultaAlterada.Status);
                Assert.Equal(consultaStatusRequest.Justificativa.ToLower(), consultaAlterada.JustificativaCancelamento.ToLower());
            }
        }

        [Fact]
        public async Task AlterarSituacaoConsulta_Status_Valido__Request_Incompleto_Espero_RegraDeNegocioExcecao()
        {
            await AutenticarAplicacao(Roles.Medico);

            HttpResponseMessage resultConsulta = await apiFactoryClient.GetAsync("api/consultas/paginados");
            PaginacaoConsulta<ConsultaResponse>? consultasPaginadas = JsonConvert.DeserializeObject<PaginacaoConsulta<ConsultaResponse>>(await resultConsulta.Content.ReadAsStringAsync());

            Assert.NotNull(consultasPaginadas);
            Assert.NotEmpty(consultasPaginadas.Registros);

            ConsultaResponse consultaResponse = consultasPaginadas.Registros.First();
            ConsultaStatusRequest consultaStatusRequest = new()
            {
                ConsultaId = consultaResponse.ConsultaId,
                Status = StatusConsultaEnum.Cancelada
            };

            string jsonContent = JsonConvert.SerializeObject(consultaStatusRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage resultAlteracao =
                await apiFactoryClient.PutAsync($"api/consultas/situacoes?ConsultaId={consultaResponse.ConsultaId}&Status=Aceita", httpContent);

            Assert.True(!resultAlteracao.IsSuccessStatusCode, "O endpoint não respondeu como esperado.");
            Assert.Equal(HttpStatusCode.BadRequest, resultAlteracao.StatusCode);

            if (!resultAlteracao.IsSuccessStatusCode)
            {
                ErroResponse? consultaAlterada = JsonConvert.DeserializeObject<ErroResponse>(await resultAlteracao.Content.ReadAsStringAsync());
                Assert.NotNull(consultaAlterada);
                Assert.Equal("Por favor forneça uma justificativa para o cancelamento.", consultaAlterada.Erro.Mensagem);
                Assert.Equal("RegraDeNegocioExcecao", consultaAlterada.Erro.Tipo);
            }
        }

        [Fact]
        public async Task Inserir_Consulta_Corretamente()
        {
            await AutenticarAplicacao(Roles.Administrador);

            ConsultaRequest consultaRequest = new()
            {
                MedicoId = 1,
                HorarioDisponivelId = 1,
                PacienteId = 2,
                Status = StatusConsultaEnum.Pendente,
                Valor = 1
            };

            string jsonContent = JsonConvert.SerializeObject(consultaRequest);
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiFactoryClient.PostAsync("api/consultas", httpContent);
            Assert.True(result.StatusCode == HttpStatusCode.Created);
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.ReasonPhrase == "Created");
        }
    }
}
