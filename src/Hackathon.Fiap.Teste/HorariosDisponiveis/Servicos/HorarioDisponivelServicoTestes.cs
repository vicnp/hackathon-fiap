using FluentAssertions;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Comandos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Pacientes.Servicos;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;

namespace Hackathon.Fiap.Teste.HorariosDisponiveis.Servicos;

public class HorariosDisponiveisServicoTestes
{
    private readonly IHorariosDisponiveisServico horariosDisponiveisServico;
    private readonly IHorariosDisponiveisRepositorio horariosDisponiveisRepositorio;
    private readonly IMedicosRepositorio medicosRepositorio;
    private readonly IPacientesRepositorio pacientesRepositorio;

    public HorariosDisponiveisServicoTestes()
    {
        // Mocks e stubs (usando um framework como NSubstitute, Moq ou outros)
        horariosDisponiveisRepositorio = Substitute.For<IHorariosDisponiveisRepositorio>();
        medicosRepositorio = Substitute.For<IMedicosRepositorio>();
        pacientesRepositorio = Substitute.For<IPacientesRepositorio>();

        horariosDisponiveisServico = new HorariosDisponiveisServico(horariosDisponiveisRepositorio, medicosRepositorio, pacientesRepositorio);
    }

    [Fact]
    public async Task Quando_GetHorarioDisponivellValido_Espero_ObjException()
    {
        HorarioDisponivel horarioDisponivel = new();
        horariosDisponiveisRepositorio.RecuperarHorarioDisponivel(1, CancellationToken.None).Returns(horarioDisponivel);
        Func<Task> act = async () => await horariosDisponiveisServico.ValidarHorarioDisponivelAsync(1, CancellationToken.None);
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Quando_GetHorarioDisponivelInvalido_Espero_ObjException()
    {
        await FluentActions.Awaiting(() => horariosDisponiveisServico.ValidarHorarioDisponivelAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()))
            .Should().ThrowAsync<RegistroNaoEncontradoExcecao>()
            .WithMessage("Horario Disponivel não encontrado!");
    }

    [Fact]
    public async Task Quando_ListarHorariosDisponiveisAsync_DeveRetornarHorariosCorretos()
    {
        // ARRANGE
        var filtro = new HorariosDisponiveisFiltro(); // Ajuste conforme necessário
        var horarioDisponivelConsulta = new HorarioDisponivelConsulta
        {
            HorarioDisponivelId = 1,
            DataHoraInicio = DateTime.Now.AddHours(1),
            DataHoraFim = DateTime.Now.AddHours(2),
            Status = StatusHorarioDisponivelEnum.Disponivel,
            MedicoId = 1,
            PacienteId = 2
        };
        
        var paginacaoConsulta = new PaginacaoConsulta<HorarioDisponivelConsulta>
        {
            Total = 1,
            Registros = new List<HorarioDisponivelConsulta> { horarioDisponivelConsulta }
        };

        horariosDisponiveisRepositorio.ListarHorariosDisponiveisAsync(Arg.Any<HorariosDisponiveisFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        // Criação do Médico e Paciente
        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();

        medicosRepositorio.RecuperarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesRepositorio.RecuperarPaciente(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);

        // ACT
        var resultado = await horariosDisponiveisServico.ListarHorariosDisponiveisAsync(filtro, CancellationToken.None);

        // ASSERT
        resultado.Total.Should().Be(1);
        resultado.Registros.Should().HaveCount(1);
        var horario = resultado.Registros.First();
        horario.Medico.Should().Be(medico);
        horario.Paciente.Should().Be(paciente);
        horario.Status.Should().Be(StatusHorarioDisponivelEnum.Disponivel);
    }

    [Fact]
    public async Task Quando_InserirHorariosDisponiveisAsync_DeveGerarHorariosCorretamente()
    {
        // ARRANGE
        var comando = new HorariosDisponiveisInserirComando
        {
            MedicoId = 1,
            DataHoraInicio = DateTime.Now.AddHours(1),
            DataHoraFim = DateTime.Now.AddHours(3)
        };

        var medico = new Medico();
        medico.SetCrm("123456");
        medicosRepositorio.RecuperarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);

        // ACT
        await horariosDisponiveisServico.InserirHorariosDisponiveisAsync(comando, CancellationToken.None);

        // ASSERT
        await horariosDisponiveisRepositorio.Received(1).InserirHorariosDisponiveisAsync(Arg.Any<List<HorarioDisponivel>>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Quando_InserirHorariosDisponiveisAsync_NaoGerarHorarios_DeveLancarExcecao()
    {
        // ARRANGE
        var comando = new HorariosDisponiveisInserirComando
        {
            MedicoId = 1,
            DataHoraInicio = DateTime.Now.AddHours(1),
            DataHoraFim = DateTime.Now.AddMinutes(10) // Intervalo muito curto para gerar horários
        };

        var medico = new Medico();
        medico.SetCrm("123456");
        medicosRepositorio.RecuperarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);

        // ACT & ASSERT
        Func<Task> act = async () => await horariosDisponiveisServico.InserirHorariosDisponiveisAsync(comando, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>().WithMessage("Nenhum horário disponível foi gerado. Verifique os horários informados.");
    }

    [Fact]
    public async Task Quando_ListarHorariosDisponiveisAsync_SeNaoExistiremHorarios_DeveRetornarListaVazia()
    {
        // ARRANGE
        var filtro = new HorariosDisponiveisFiltro();
        var paginacaoConsultaVazia = new PaginacaoConsulta<HorarioDisponivelConsulta>
        {
            Total = 0,
            Registros = new List<HorarioDisponivelConsulta>()
        };

        horariosDisponiveisRepositorio.ListarHorariosDisponiveisAsync(Arg.Any<HorariosDisponiveisFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsultaVazia);

        // ACT
        var resultado = await horariosDisponiveisServico.ListarHorariosDisponiveisAsync(filtro, CancellationToken.None);

        // ASSERT
        resultado.Total.Should().Be(0);
        resultado.Registros.Should().BeEmpty();
    }
}
