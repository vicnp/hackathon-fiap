using FluentAssertions;
using Hackathon.Fiap.Domain.Medicos.Entidades;

namespace Hackathon.Fiap.Teste.Medicos.Entidades;

public class MedicoTestes
{
    [Fact]
    public void Quando_CriarMedico_ComEspecialidade_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        var especialidade = new Especialidade 
        { 
            EspecialidadeId = 1, 
            NomeEspecialidade = "Cardiologia", 
            DescricaoEspecialidade = "Especialidade do coração" 
        };
        var crm = "123456";

        // ACT
        var medico = new Medico();
        medico.SetCrm(crm);
        medico.SetEspecialidade(especialidade);

        // ASSERT
        medico.Crm.Should().Be(crm);
        medico.Especialidade.Should().Be(especialidade);
        medico.Especialidade.NomeEspecialidade.Should().Be("Cardiologia");
    }

    [Fact]
    public void Quando_SetEspecialidade_ComNull_DeveLancarArgumentNullException()
    {
        // ARRANGE
        var medico = new Medico();

        // ACT & ASSERT
        Action act = () => medico.SetEspecialidade(null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'especialidade')");
    }

    [Fact]
    public void Quando_SetCrm_ComValorValido_DeveAtualizarPropriedade()
    {
        // ARRANGE
        var medico = new Medico();
        var crm = "654321";

        // ACT
        medico.SetCrm(crm);

        // ASSERT
        medico.Crm.Should().Be(crm);
    }
} 