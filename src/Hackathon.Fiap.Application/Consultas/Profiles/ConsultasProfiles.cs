using AutoMapper;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Consultas.Profiles
{
    public class ConsultasProfiles : Profile
    {
        public ConsultasProfiles()
        {
            CreateMap<ConsultaRequest, ConsultaInserirFiltro>().ReverseMap();
            CreateMap<ConsultaListarRequest, ConsultasListarFiltro>().ReverseMap();
            CreateMap<PaginacaoConsulta<Consulta>, PaginacaoConsulta<ConsultaResponse>>().ReverseMap();
            CreateMap<Consulta, ConsultaResponse>().ReverseMap();
        }
    }
}
