using AutoMapper;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;

namespace Hackathon.Fiap.Application.Consultas.Profiles
{
    public class ConsultasProfiles : Profile
    {
        public ConsultasProfiles()
        {
            CreateMap<ConsultaListarRequest, ConsultasListarFiltro>().ReverseMap();
            CreateMap<PaginacaoConsulta<Consulta>, PaginacaoConsulta<ConsultaResponse>>().ReverseMap();
            CreateMap<Consulta, ConsultaResponse>().ReverseMap();
        }
    }
}
