using AutoMapper;
using Hackathon.Fiap.Application.Usuarios.Interfaces;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;

namespace Hackathon.Fiap.Application.Usuarios
{
    public class UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio, IMapper mapper) : IUsuariosAppServico
    {
        public PaginacaoConsulta<UsuarioResponse> ListarPacientes(UsuarioListarRequest request)
        {
            PaginacaoConsulta<Usuario> response = usuariosRepositorio.ListarUsuarios(request);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(response);
        }
    }
}
