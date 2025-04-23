using FluentAssertions;
using Hackathon.Fiap.Domain.Consultas.Consultas;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;

namespace Hackathon.Fiap.Teste.Consultas.Servicos;

public class ConsultasServicoTestes
{
    private readonly IConsultaServico consultaServico;
    private readonly IConsultasRepositorio consultasRepositorio;
    private readonly IMedicosRepositorio medicosRepositorio;
    private readonly IPacientesRepositorio pacientesRepositorio;

    public ConsultasServicoTestes()
    {
        consultasRepositorio = Substitute.For<IConsultasRepositorio>();
        medicosRepositorio = Substitute.For<IMedicosRepositorio>();
        pacientesRepositorio = Substitute.For<IPacientesRepositorio>();

        consultaServico = new ConsultasServico(consultasRepositorio, medicosRepositorio, pacientesRepositorio);
    }

    [Fact]
    public async Task Quando_ListarConsultasAsync_DeveRetornarConsultasCorretas()
    {
        // ARRANGE
        var filtro = new ConsultasListarFiltro();
        var consultaConsulta = new ConsultaConsulta
        {
            IdConsulta = 1,
            DataHora = DateTime.Now.AddDays(1),
            Valor = 150.00,
            Status = StatusConsultaEnum.Pendente.ToString(),
            JustificativaCancelamento = "",
            CriadoEm = DateTime.Now,
            IdHorariosDisponiveis = 1,
            IdMedico = 1,
            IdPaciente = 1
        };

        var paginacaoConsulta = new PaginacaoConsulta<ConsultaConsulta>
        {
            Total = 1,
            Registros = new List<ConsultaConsulta> { consultaConsulta }
        };

        consultasRepositorio.ListarConsultasAsync(Arg.Any<ConsultasListarFiltro>(), Arg.Any<CancellationToken>())
            .Returns(paginacaoConsulta);

        var medico = new Medico();
        medico.SetCrm("123456");
        var paciente = new Paciente();

        medicosRepositorio.RecuperarMedico(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(medico);
        pacientesRepositorio.RecuperarPaciente(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(paciente);

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
            IdConsulta = 1,
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
            IdConsulta = 1,
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
            IdConsulta = 1,
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
                    IdConsulta = 1,
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