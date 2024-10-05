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

        [Fact]
        public async Task Cria_Contatos_Corretamente()
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

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Listar_Contatos_Corretamente()
        {
            var result = await apiFactoryClient.GetAsync("api/contatos/itens");

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Editar_Contatos_Corretamente()
        {
            int idEdicao = 1;

            var contato = new ContatoCrudRequest
            {
                Nome = "Jorge da Silva Sauro",
                Email = "jorge@dino.com.br",
                DDD = 11,
                Telefone = "912345678"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(contato), Encoding.UTF8, "application/json");

            var result = await apiFactoryClient.PutAsync($"api/contatos/{idEdicao}", requestContent);

            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
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
