using AutoMapper;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Application.Pacientes.Profiles
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