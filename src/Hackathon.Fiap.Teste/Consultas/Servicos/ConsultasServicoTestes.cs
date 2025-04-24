using FluentAssertions;
using Hackathon.Fiap.Domain.Consultas.Consultas;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;

namespace Hackathon.Fiap.Teste.Consultas.Servicos;

public class ConsultasServicoTestes
{
    private readonly IConsultasRepositorio consultasRepositorio;
    private readonly IConsultasServico consultaServico;
    private readonly IMedicosServico medicosServico;
    private readonly IPacientesServicos pacientesServicos;
    private readonly IHorariosDisponiveisServico horariosDisponiveisServico;
    private readonly IHorariosDisponiveisRepositorio horariosDisponiveisRepositorio;
    public ConsultasServicoTestes()
    {
        consultaServico = Substitute.For<IConsultasServico>();
        medicosServico = Substitute.For<IMedicosServico>();
        pacientesServicos = Substitute.For<IPacientesServicos>();
        horariosDisponiveisServico = Substitute.For<IHorariosDisponiveisServico>();
        consultasRepositorio = Substitute.For<IConsultasRepositorio>();
        horariosDisponiveisRepositorio = Substitute.For<IHorariosDisponiveisRepositorio>();
        consultaServico = new ConsultasServico(consultasRepositorio, medicosServico, horariosDisponiveisServico, horariosDisponiveisRepositorio, pacientesServicos);
    }

    [Fact]
    public async Task Quando_ListarConsultasAsync_DeveRetornarConsultasCorretas()
    {
        // ARRANGE
        var filtro = new ConsultasListarFiltro();
        var consultaConsulta = new ConsultaConsulta
        {
            ConsultaId = 1,
            Valor = 150.00,
            Status = StatusConsultaEnum.Pendente.ToString(),
            JustificativaCancelamento = "",
            CriadoEm = DateTime.Now,
            HorarioDisponivelId = 1,
            MedicoId = 1,
            PacienteId = 1
        };

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 1,
            Registros = [consultaConsulta]
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();

        medicosServico.ValidarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesServicos.ValidarPacienteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);

        // ACT
        var resultado = await consultaServico.ListarConsultasAsync(filtro, CancellationToken.None);

        // ASSERT
        resultado.Total.Should().Be(1);
        resultado.Registros.Should().HaveCount(1);
        var consulta = resultado.Registros.First();
        consulta.Medico.Should().Be(medico);
        consulta.Paciente.Should().Be(paciente);
        consulta.Status.Should().Be(StatusConsultaEnum.Pendente);
    }

    [Fact]
    public async Task Quando_InserirConsultasAsync_DeveRetornarErro()
    {
        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();
        var horarioDisponivel = new HorarioDisponivel(
            horarioDisponivelId: 1, 
            dataHoraInicio: DateTime.Now, 
            dataHoraFim: DateTime.Now, 
            status: StatusHorarioDisponivelEnum.Reservado);
        ConsultaInserirFiltro filtro = new();

        medicosServico.ValidarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesServicos.ValidarPacienteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);
        horariosDisponiveisServico.ValidarHorarioDisponivelAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(horarioDisponivel);

        Func <Task> act = async () => await consultaServico.InserirConsultaAsync(filtro, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("O horário selecionado não está mais disponível!");
    }

    [Fact]
    public async Task Quando_InserirConsultasAsync_DeveRetornarNenhumErro()
    {
        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();
        var horarioDisponivel = new HorarioDisponivel(
            horarioDisponivelId: 1,
            dataHoraInicio: DateTime.Now,
            dataHoraFim: DateTime.Now,
            status: StatusHorarioDisponivelEnum.Disponivel);
        ConsultaInserirFiltro filtro = new();

        medicosServico.ValidarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesServicos.ValidarPacienteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);
        horariosDisponiveisServico.ValidarHorarioDisponivelAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(horarioDisponivel);

        Func<Task> act = async () => await consultaServico.InserirConsultaAsync(filtro, CancellationToken.None);
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Quando_ListarConsultasAsync_DeveRetornarConsultasStatusNullIncorreto()
    {
        // ARRANGE
        var filtro = new ConsultasListarFiltro();
        var consultaConsulta = new ConsultaConsulta
        {
            ConsultaId = 1,
            Valor = 150.00,
            Status = null,
            JustificativaCancelamento = "",
            CriadoEm = DateTime.Now,
            HorarioDisponivelId = 1,
            MedicoId = 1,
            PacienteId = 1
        };

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 1,
            Registros = [consultaConsulta]
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();

        medicosServico.ValidarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesServicos.ValidarPacienteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.ListarConsultasAsync(filtro, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("Não foi possível identificar a situação da consulta.");
    }

    [Fact]
    public async Task QuandoListarConsultasAsyncDeveRetornarConsultasNull()
    {
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Aceita,
            JustificativaCancelamento = ""
        };

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 0,
            Registros = []
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Pendente, CancellationToken.None);
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Quando_ListarConsultasAsync_DeveRetornarConsultasStatusincorreto()
    {
        // ARRANGE
        var filtro = new ConsultasListarFiltro();
        var consultaConsulta = new ConsultaConsulta
        {
            ConsultaId = 1,
            Valor = 150.00,
            Status = "nsdaull",
            JustificativaCancelamento = "",
            CriadoEm = DateTime.Now,
            HorarioDisponivelId = 1,
            MedicoId = 1,
            PacienteId = 1
        };

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 1,
            Registros = [consultaConsulta]
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();

        medicosServico.ValidarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesServicos.ValidarPacienteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.ListarConsultasAsync(filtro, CancellationToken.None);
        await act.Should().ThrowAsync<FalhaConversaoExcecao>()
            .WithMessage("Não foi possivel determinar a situação da consulta.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Quando_AtualizarStatusConsulta_ParaCancelada_SemJustificativa_DeveLancarExcecao(string JustificativaCancelamento)
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Pendente,
            JustificativaCancelamento = JustificativaCancelamento
        };

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Cancelada, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("Por favor forneça uma justificativa para o cancelamento.");
    }

    [Fact]
    public async Task Quando_AtualizarStatusConsulta_ParaCancelada_ComJustificativa_NaoExcecao()
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Pendente,
            JustificativaCancelamento = "asd asd"
        };

        // Configurando o mock para retornar 1 (sucesso)
        consultasRepositorio.AtualizarStatusConsultaAsync(Arg.Any<Consulta>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(1));

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 1,
            Registros =
            [
                new() {
                    ConsultaId = 1,
                    Status = StatusConsultaEnum.Aceita.ToString()
                }
            ]
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);


        await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Cancelada, CancellationToken.None);
    }

    [Fact]
    public async Task Quando_AtualizarStatusConsulta_ParaAceita_QuandoJaCancelada_DeveLancarExcecao()
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Cancelada,
            JustificativaCancelamento = "Cancelada pelo paciente"
        };

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Aceita, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("A consulta está cancelada.");
    }

    [Fact]
    public async Task Quando_AtualizarStatusConsulta_ParaAceita_QuandoJaRecusada_DeveLancarExcecao()
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Recusada,
            JustificativaCancelamento = "Recusada pelo paciente"
        };

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Aceita, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("A consulta está recusada.");
    }

    [Fact]
    public async Task Quando_AtualizarStatusConsulta_ParaAceita_QuandoPendente_DeveAtualizarCorretamente()
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Pendente
        };

        // Configurando o mock para retornar 1 (sucesso)
        consultasRepositorio.AtualizarStatusConsultaAsync(Arg.Any<Consulta>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(1));

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 1,
            Registros =
            [
                new() {
                    ConsultaId = 1,
                    Status = StatusConsultaEnum.Aceita.ToString()
                }
            ]
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        // ACT
        var resultado = await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Aceita, CancellationToken.None);

        // ASSERT
        resultado.Should().NotBeNull();
        resultado!.Status.Should().Be(StatusConsultaEnum.Aceita);
        await consultasRepositorio.Received(1).AtualizarStatusConsultaAsync(Arg.Any<Consulta>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Quando_ConsultaAceitastatusPendenteDeveLancarExcecao()
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Aceita
        };

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Aceita, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("A consulta não pode ser aceita.");
    }
}