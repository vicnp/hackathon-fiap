using AutoMapper;
using Hackathon.Fiap.Application.Usuarios.Interfaces;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Usuarios
{
    public class UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio, IMapper mapper) : IUsuariosAppServico
    {
        public async Task<PaginacaoConsulta<UsuarioResponse>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct)
        {
            var filtro = mapper.Map<UsuarioListarFiltro>(request);
            PaginacaoConsulta<Usuario> response = await usuariosRepositorio.ListarUsuariosAsync(filtro, ct);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(response);
        }
    }
}
