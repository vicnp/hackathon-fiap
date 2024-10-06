using Contatos.Reponses;
using Contatos.Requests;
using FluentAssertions;
using Integracao.TechChallengeApi.Factory;
using Newtonsoft.Json;
using System.Text;

namespace Integracao.ContatosIntegracaoTeste
{

    public class ContatosIntegracaoTeste : IClassFixture<TechChallengeApiFactory>
    {
        private readonly TechChallengeApiFactory techChallengeApiFactory;
        private readonly HttpClient apiFactoryClient;

        public ContatosIntegracaoTeste(TechChallengeApiFactory techChallengeApiFactory)
        {
            this.techChallengeApiFactory = techChallengeApiFactory;
            apiFactoryClient = techChallengeApiFactory.CreateClient();
        }

        private async Task<HttpResponseMessage> CriaContato()
        {
            var contato = new ContatoCrudRequest
            {
                Nome = "Jorge da Silva Sauro",
                Email = "jorge@dino.com.br",
                DDD = 11,
                Telefone = "912345678"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(contato), Encoding.UTF8, "application/json");

            var result = await apiFactoryClient.PostAsync("api/contatos", requestContent);
            return result;
        }

        [Fact]
        public async Task Cria_Contatos_Corretamente()
        {
            HttpResponseMessage result = await CriaContato();

            ContatoResponse? contato = JsonConvert.DeserializeObject<ContatoResponse>(await result.Content.ReadAsStringAsync());

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            contato.Should().NotBeNull();
            contato.Regiao.Descricao.Should().Be("Sudeste");
        }


        [Fact]
        public async Task Listar_Contatos_Corretamente()
        {
            _ = await CriaContato();
            HttpResponseMessage result = await apiFactoryClient.GetAsync("api/contatos/itens");

            List<ContatoResponse>? contatos = JsonConvert.DeserializeObject<List<ContatoResponse>>(await result.Content.ReadAsStringAsync());

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            contatos.Should().NotBeNull();
            contatos.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Editar_Contatos_Corretamente()
        {

            HttpResponseMessage contatoCriado = await CriaContato();

            ContatoResponse? contato = JsonConvert.DeserializeObject<ContatoResponse>(await contatoCriado.Content.ReadAsStringAsync());

            int idEdicao = -1;
            if (contato!.Id.HasValue)
            {
                idEdicao = contato.Id.Value;
                contato.Nome = "Contato Editado";
            }

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(contato), Encoding.UTF8, "application/json");

            HttpResponseMessage result = await apiFactoryClient.PutAsync($"api/contatos/{idEdicao}", requestContent);

            ContatoResponse? contatoEditado = JsonConvert.DeserializeObject<ContatoResponse>(await result.Content.ReadAsStringAsync());

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            contatoEditado.Should().NotBeNull();
            contatoEditado.Nome.Should().Be(contato.Nome);
        }

        [Fact]
        public async Task Deletar_Contatos_Corretamente()
        {
            int idDelecao = 1;

            var result = await apiFactoryClient.DeleteAsync($"api/contatos/{idDelecao}");

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
