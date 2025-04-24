using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Comandos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;

public interface IHorariosDisponiveisServico
{
    Task<PaginacaoConsulta<Entidades.HorarioDisponivel>> ListarHorariosDisponiveisAsync(HorariosDisponiveisFiltro filtro, CancellationToken ct);
    Task InserirHorariosDisponiveisAsync(HorariosDisponiveisInserirComando comando, CancellationToken ct);
    Task<HorarioDisponivel> ValidarHorarioDisponivelAsync(int horarioDisponivelId, CancellationToken cancellationToken);
}