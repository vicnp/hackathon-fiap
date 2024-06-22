using AutoMapper;
using TC_Application.Usuarios.Interfaces;
using TC_DataTransfer.Usuarios.Request;
using TC_DataTransfer.Usuarios.Response;
using TC_Domain.Usuarios.Entidades;
using TC_Domain.Usuarios.Repositorios;
using TC_IOC.Bibliotecas;

namespace TC_Application.Usuarios
{
    public class UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio, IMapper mapper) : IUsuariosAppServico
    {

        public PaginacaoConsulta<UsuarioResponse> ListarUsuarios(UsuarioListarRequest request)
        {
            PaginacaoConsulta<Usuario> usuarios = usuariosRepositorio.ListarUsuarios(request);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(usuarios);
        }
    }
}
