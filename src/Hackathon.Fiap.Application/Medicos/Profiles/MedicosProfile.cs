using AutoMapper;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;

namespace Hackathon.Fiap.Application.Medicos.Profiles
{
    public class MedicosProfile : Profile
    {
        public MedicosProfile()
        {
            CreateMap<MedicosPaginacaoRequest, MedicosPaginacaoFiltro>();
            CreateMap<Medico, MedicoResponse>();
            CreateMap<PaginacaoConsulta<Medico>, PaginacaoConsulta<MedicoResponse>>();
            CreateMap<Especialidade, EspecialidadeResponse>();
        }
    }
}