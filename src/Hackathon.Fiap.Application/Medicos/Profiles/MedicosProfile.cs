using AutoMapper;
using Medicos.Entidades;
using Medicos.Repositorios.Filtros;
using Medicos.Requests;
using Medicos.Responses;
using Utils;

namespace Medicos.Profiles
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