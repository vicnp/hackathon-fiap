using AutoMapper;
using Hackathon.Fiap.DataTransfer.Pacientes.Requests;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;

namespace Hackathon.Fiap.Application.Pacientes.Profiles
{
    public class PacientesProfile : Profile
    {
        public PacientesProfile()
        {
            CreateMap<Paciente, PacienteResponse>().ReverseMap();
            CreateMap<PaginacaoConsulta<Paciente>, PaginacaoConsulta<PacienteResponse>>().ReverseMap();
            CreateMap<PacienteListarRequest, UsuarioListarFiltro>().ReverseMap();
        }
    }
}