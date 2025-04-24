using AutoMapper;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Usuarios.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<PaginacaoConsulta<Usuario>, PaginacaoConsulta<UsuarioResponse>>();
            CreateMap<UsuarioListarRequest, UsuarioListarFiltro>().ReverseMap();
            CreateMap<UsuarioCadastroRequest, UsuarioCadastroComando>().ReverseMap();
        }
    }
}
