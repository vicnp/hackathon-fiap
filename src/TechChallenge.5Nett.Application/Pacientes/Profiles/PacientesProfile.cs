using AutoMapper;
using Pacientes.Entidades;
using Pacientes.Responses;
using Utils;

namespace Pacientes.Profiles
{
    public class PacientesProfile : Profile
    {
        public PacientesProfile()
        {
            CreateMap<Paciente, PacienteResponse>().ReverseMap();
            CreateMap<PaginacaoConsulta<Paciente>, PaginacaoConsulta<PacienteResponse>>().ReverseMap();
        }
    }
}