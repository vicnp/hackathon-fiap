using AutoMapper;
using LHS_DataTransfer.Usuarios.Response;
using LHS_Domain.Usuarios.Entidades;
using LHS_IOT.Bibliotecas;

namespace LHS_Application.Usuarios.Profiles
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
