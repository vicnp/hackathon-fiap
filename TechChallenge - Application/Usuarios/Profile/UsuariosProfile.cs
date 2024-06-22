using AutoMapper;
using TC_DataTransfer.Usuarios.Response;
using TC_Domain.Usuarios.Entidades;
using TC_IOC.Bibliotecas;

namespace TC_Application.Usuarios.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<PaginacaoConsulta<Usuario>, PaginacaoConsulta<UsuarioResponse>>();
        }
    }
}
