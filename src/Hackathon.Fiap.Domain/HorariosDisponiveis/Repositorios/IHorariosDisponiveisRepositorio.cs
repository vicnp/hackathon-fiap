using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;

public interface IHorariosDisponiveisRepositorio
{
    Task AtualizarStatusHorarioDisponivel(StatusHorarioDisponivelEnum statusHorarioDisponivel, int horarioDisponivelId);
    Task InserirHorariosDisponiveisAsync(IEnumerable<HorarioDisponivel> horarios, CancellationToken ct);
    Task<PaginacaoConsulta<HorarioDisponivelConsulta>> ListarHorariosDisponiveisAsync(HorariosDisponiveisFiltro filtro, CancellationToken ct);
    Task<HorarioDisponivel?> RecuperarHorarioDisponivel(int horarioDisponivelId, CancellationToken ct);
}