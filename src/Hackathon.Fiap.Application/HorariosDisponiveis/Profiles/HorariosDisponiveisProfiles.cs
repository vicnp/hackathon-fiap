using AutoMapper;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Responses;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Comandos;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.HorariosDisponiveis.Profiles;

public class HorariosDisponiveisProfiles : Profile
{
    public HorariosDisponiveisProfiles()
    {
        CreateMap<HorarioDisponivelListarRequest, HorariosDisponiveisFiltro>().ReverseMap();
        CreateMap<HorarioDisponivelInserirRequest, HorariosDisponiveisInserirComando>().ReverseMap();
        CreateMap<PaginacaoConsulta<Domain.HorariosDisponiveis.Entidades.HorarioDisponivel>, PaginacaoConsulta<HorarioDisponivelResponse>>().ReverseMap();
        CreateMap<Domain.HorariosDisponiveis.Entidades.HorarioDisponivel, HorarioDisponivelResponse>().ReverseMap();
    }
}