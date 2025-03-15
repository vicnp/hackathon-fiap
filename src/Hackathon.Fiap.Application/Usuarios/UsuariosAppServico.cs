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
        public async Task<PaginacaoConsulta<UsuarioResponse>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<Usuario> response = await usuariosRepositorio.ListarUsuariosAsync(request, ct);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(response);
        }
    }
}
