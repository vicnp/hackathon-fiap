using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;

public interface IHorariosDisponiveisRepositorio
{
    Task<PaginacaoConsulta<HorarioDisponivelConsulta>> ListarHorariosDisponiveisAsync(HorariosDisponiveisFiltro filtro, CancellationToken ct);

    Task InserirHorariosDisponiveisAsync(IEnumerable<HorarioDisponivel> horarios, CancellationToken ct);
}