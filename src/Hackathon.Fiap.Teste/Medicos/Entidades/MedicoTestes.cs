using FluentAssertions;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Teste.Medicos.Entidades;

public class MedicoTestes
{
    [Fact]
    public void Quando_CriarMedico_ComEspecialidade_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        Especialidade especialidade = new()
        {
            EspecialidadeId = 1,
            NomeEspecialidade = "Cardiologia",
            DescricaoEspecialidade = "Especialidade do coração"
        };
        string crm = "123456";

        // ACT
        Medico medico = new();
        medico.SetCrm(crm);
        medico.SetEspecialidade(especialidade);

        // ASSERT
        medico.Crm.Should().Be(crm);
        medico.Especialidade.Should().Be(especialidade);
        medico.Especialidade.NomeEspecialidade.Should().Be("Cardiologia");
    }

    [Fact]
    public void Quando_CriarMedico_ComEspecialidade_Construtor_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        Especialidade especialidade = new()
        {
            EspecialidadeId = 1,
            NomeEspecialidade = "Cardiologia",
            DescricaoEspecialidade = "Especialidade do coração"
        };
        string crm = "123456";

        // ACT
        Medico medico = new(1, "dois", "tres", "quatro", "cinco", TipoUsuario.Medico);
        medico.SetCrm(crm);
        medico.SetEspecialidade(especialidade);

        // ASSERT
        medico.Crm.Should().Be(crm);
        medico.Especialidade.Should().Be(especialidade);
        medico.Especialidade.NomeEspecialidade.Should().Be("Cardiologia");
    }

    [Fact]
    public void Quando_CriarMedico_ComEspecialidade_Construtor_crm_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        Especialidade especialidade = new()
        {
            EspecialidadeId = 1,
            NomeEspecialidade = "Cardiologia",
            DescricaoEspecialidade = "Especialidade do coração"
        };
        string crm = "123456";

        // ACT
        Medico medico = new("dois", "tres", "quatro", "cinco", "seis", TipoUsuario.Medico);
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
        Medico medico = new();

        // ACT & ASSERT
        Action act = () => medico.SetEspecialidade(null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'especialidade')");
    }

    [Fact]
    public void Quando_SetCrm_ComValorValido_DeveAtualizarPropriedade()
    {
        // ARRANGE
        Medico medico = new();
        string crm = "654321";

        // ACT
        medico.SetCrm(crm);

        // ASSERT
        medico.Crm.Should().Be(crm);
    }
}