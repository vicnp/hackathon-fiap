using FluentAssertions;
using Hackathon.Fiap.Domain.Medicos.Entidades;

namespace Hackathon.Fiap.Teste.Medicos.Entidades
{
    public class EspecialidadeTestes
    {
        [Fact]
        public void Quando_criar_instacia_espero_objeto_valido()
        {
            Especialidade especialidade = new("Espc", "Desc");
            especialidade.Should().NotBeNull();
            especialidade.DescricaoEspecialidade.Should().Be("Desc");
            especialidade.NomeEspecialidade.Should().Be("Espc");
        }
    }
}
