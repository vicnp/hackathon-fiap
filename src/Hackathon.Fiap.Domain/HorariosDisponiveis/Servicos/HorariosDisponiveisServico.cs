using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Comandos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos;

public class HorariosDisponiveisServico(IHorariosDisponiveisRepositorio horariosDisponiveisRepositorio, IMedicosRepositorio medicosRepositorio, IPacientesRepositorio pacientesRepositorio) : IHorariosDisponiveisServico
{

    public async Task<HorarioDisponivel> ValidarHorarioDisponivelAsync(int horarioDisponivelId, CancellationToken cancellationToken)
    {
        return await horariosDisponiveisRepositorio.RecuperarHorarioDisponivel(horarioDisponivelId, cancellationToken) ?? throw new RegistroNaoEncontradoExcecao("Horario Disponivel não encontrado!");
    }

    public async Task<PaginacaoConsulta<HorarioDisponivel>> ListarHorariosDisponiveisAsync(HorariosDisponiveisFiltro filtro, CancellationToken ct)
    {
        PaginacaoConsulta<HorarioDisponivelConsulta> paginacaoConsulta = await horariosDisponiveisRepositorio.ListarHorariosDisponiveisAsync(filtro, ct);
        List<HorarioDisponivel> horariosDisponiveis = [];
        PaginacaoConsulta<HorarioDisponivel> response = new()
        {
            Total = paginacaoConsulta.Total,
            Registros = []
        };

        foreach (HorarioDisponivelConsulta itemHoratio in paginacaoConsulta.Registros)
        {
            HorarioDisponivel horarioDisponivel = new(itemHoratio.HorarioDisponivelId,
                itemHoratio.DataHoraInicio,
                itemHoratio.DataHoraFim,
                itemHoratio.Status);

            if (itemHoratio.MedicoId > 0)
            {
                Medico? medico = await medicosRepositorio.RecuperarMedicoAsync(itemHoratio.MedicoId, ct: ct);

                horarioDisponivel.SetMedico(medico!);
            }

            if (itemHoratio.PacienteId > 0)
            {
                Paciente? paciente = await pacientesRepositorio.RecuperarPaciente(itemHoratio.PacienteId, ct: ct);

                horarioDisponivel.SetPaciente(paciente);
            }

            horariosDisponiveis.Add(horarioDisponivel);
        }
        response.Registros = horariosDisponiveis;
        return response;
    }

    public async Task InserirHorariosDisponiveisAsync(HorariosDisponiveisInserirComando comando, CancellationToken ct)
    {
        var horarios = new List<HorarioDisponivel>();

        // Ajusta o intervalo para múltiplos de 30 minutos
        DateTime dataHoraAtual = comando.DataHoraInicio;

        Medico? medico = await medicosRepositorio.RecuperarMedicoAsync(comando.MedicoId, ct: ct);
        RegistroNaoEncontradoExcecao.LancarExcecaoSeNulo(medico, "Medico não encontrado.");

        while (dataHoraAtual.AddMinutes(30) <= comando.DataHoraFim)
        {
            horarios.Add(new HorarioDisponivel
            {
                Medico = medico,
                DataHoraInicio = dataHoraAtual,
                DataHoraFim = dataHoraAtual.AddMinutes(30),
                Status = StatusHorarioDisponivelEnum.Disponivel
            });

            dataHoraAtual = dataHoraAtual.AddMinutes(30);
        }

        if (horarios.Count == 0)
            throw new RegraDeNegocioExcecao("Nenhum horário disponível foi gerado. Verifique os horários informados.");

        await horariosDisponiveisRepositorio.InserirHorariosDisponiveisAsync(horarios, ct);
    }


}