using FluentAssertions;
using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Hackathon.Fiap.Infra.Consultas;
using Hackathon.Fiap.Infra.Consultas.Consultas;
using Hackathon.Fiap.Infra.HorariosDisponiveis;
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
            DataHora = DateTime.Now.AddDays(1),
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
    public async Task Quando_AtualizarStatusConsulta_ParaCancelada_SemJustificativa_DeveLancarExcecao()
    {
        // ARRANGE
        var consulta = new Consulta
        {
            ConsultaId = 1,
            Status = StatusConsultaEnum.Pendente,
            JustificativaCancelamento = ""
        };

        // ACT & ASSERT
        Func<Task> act = async () => await consultaServico.AtualizarStatusConsultaAsync(consulta, StatusConsultaEnum.Cancelada, CancellationToken.None);
        await act.Should().ThrowAsync<RegraDeNegocioExcecao>()
            .WithMessage("Por favor forneça uma justificativa para o cancelamento.");
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
            Registros = new List<ConsultaConsulta>
            {
                new ConsultaConsulta
                {
                    ConsultaId = 1,
                    Status = StatusConsultaEnum.Aceita.ToString()
                }
            }
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
} 