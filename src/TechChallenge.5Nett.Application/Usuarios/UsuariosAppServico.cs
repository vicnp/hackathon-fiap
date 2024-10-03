using AutoMapper;
using Usuarios.Entidades;
using Usuarios.Interfaces;
using Usuarios.Repositorios;
using Usuarios.Request;
using Usuarios.Response;
using Utils;

namespace Usuarios
{
    public class UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio, IMapper mapper) : IUsuariosAppServico
    {
        public PaginacaoConsulta<UsuarioResponse> ListarUsuarios(UsuarioListarRequest request)
        {
            PaginacaoConsulta<Usuario> response = usuariosRepositorio.ListarUsuarios(request);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(response);
        }
    }
}
