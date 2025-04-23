using AutoMapper;
using Hackathon.Fiap.Application.HorariosDisponiveis.Interfaces;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Responses;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Comandos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.HorariosDisponiveis.Servicos;

public class HorariosDisponiveisAppServico(IMapper mapper, IHorariosDisponiveisServico horariosDisponiveisServico) : IHorariosDisponiveisAppServico
{
    public async Task<PaginacaoConsulta<HorarioDisponivelResponse>> ListarHorariosDisponiveisAsync(HorarioDisponivelListarRequest request, CancellationToken ct)
    {
        HorariosDisponiveisFiltro filtro = mapper.Map<HorariosDisponiveisFiltro>(request);

        PaginacaoConsulta<HorarioDisponivel> horarioDisponivel = await horariosDisponiveisServico.ListarHorariosDisponiveisAsync(filtro, ct);

        PaginacaoConsulta<HorarioDisponivelResponse> response = mapper.Map<PaginacaoConsulta<HorarioDisponivelResponse>>(horarioDisponivel);
        return response;
    }

    public async Task InserirHorariosDisponiveisAsync(HorarioDisponivelInserirRequest request, CancellationToken ct)
    {
        HorariosDisponiveisInserirComando comando = mapper.Map<HorariosDisponiveisInserirComando>(request);
        
        await horariosDisponiveisServico.InserirHorariosDisponiveisAsync(comando, ct);
    }
}