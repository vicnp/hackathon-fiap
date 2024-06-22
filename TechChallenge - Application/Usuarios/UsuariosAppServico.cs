using AutoMapper;
using LHS_Application.Usuarios.Interfaces;
using LHS_DataTransfer.Usuarios.Request;
using LHS_DataTransfer.Usuarios.Response;
using LHS_Domain.Usuarios.Entidades;
using LHS_Domain.Usuarios.Repositorios;
using LHS_IOT.Bibliotecas;

namespace LHS_Application.Usuarios
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
