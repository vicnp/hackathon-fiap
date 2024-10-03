using AutoMapper;
using Usuarios.Entidades;
using Usuarios.Response;
using Utils;

namespace Usuarios.Profiles
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
