using FluentAssertions;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.Medicos.Entidades;

namespace Hackathon.Fiap.Teste.HorariosDisponiveis.Entidades;

public class HorarioDisponivelTestes
{
    [Fact]
    public void Quando_CriarHorarioDisponivel_ComMedico_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        int id = 1;
        DateTime dataHoraInicio = new(2025, 03, 31, 9, 0, 0);
        DateTime dataHoraFim = new(2025, 03, 31, 10, 0, 0);
        StatusHorarioDisponivelEnum status = StatusHorarioDisponivelEnum.Disponivel;
        
        // Criando a especialidade
        var especialidade = new Especialidade { EspecialidadeId = 1, NomeEspecialidade = "Cardiologia", DescricaoEspecialidade = "Especialidade de coração" };
        
        // Criando o médico com a especialidade e CRM
        var medico = new Medico();
        medico.SetCrm("123456");
        medico.SetEspecialidade(especialidade);

        // ACT
        var horarioDisponivel = new HorarioDisponivel(id, dataHoraInicio, dataHoraFim, status);
        horarioDisponivel.SetMedico(medico);

        // ASSERT
        horarioDisponivel.Medico.Should().Be(medico);
        horarioDisponivel.Medico.Crm.Should().Be("123456");
        horarioDisponivel.Medico.Especialidades.Should().Be(especialidade);
    }

    [Fact]
    public void Quando_SetEspecialidadeComNull_DeveLancarArgumentNullException()
    {
        // ARRANGE
        Medico medico = new();
        Especialidade? especialidade = default;
        // ACT & ASSERT
        Action act = () => medico.SetEspecialidade(especialidade);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'especialidade')");
    }

    [Fact]
    public void Quando_CriarHorarioDisponivel_ComMedico_ComEspecialidade_DeveInicializarCorretamente()
    {
        // ARRANGE
        int id = 1;
        DateTime dataHoraInicio = new(2025, 03, 31, 9, 0, 0);
        DateTime dataHoraFim = new(2025, 03, 31, 10, 0, 0);
        StatusHorarioDisponivelEnum status = StatusHorarioDisponivelEnum.Disponivel;
        
        // Criando especialidade
        var especialidade = new Especialidade { EspecialidadeId = 1, NomeEspecialidade = "Pediatria", DescricaoEspecialidade = "Cuidados com crianças" };
        
        // Criando medico
        var medico = new Medico();
        medico.SetCrm("987654");
        medico.SetEspecialidade(especialidade);

        // ACT
        var horarioDisponivel = new HorarioDisponivel(id, dataHoraInicio, dataHoraFim, status);
        horarioDisponivel.SetMedico(medico);

        // ASSERT
        horarioDisponivel.Medico.Should().NotBeNull();
        horarioDisponivel.Medico.Crm.Should().Be("987654");
        horarioDisponivel.Medico.Especialidades.Should().Be(especialidade);
    }
}
