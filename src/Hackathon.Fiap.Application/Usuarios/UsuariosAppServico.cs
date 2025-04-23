using AutoMapper;
using Hackathon.Fiap.Application.Usuarios.Interfaces;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Usuarios.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Usuarios
{
    public class UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio, IUsuariosServico usuarioServico, IMapper mapper) : IUsuariosAppServico
    {
        public async Task<UsuarioResponse> CadastrarUsuarioAsync(UsuarioCadastroRequest request, CancellationToken ct)
        {
            UsuarioCadastroComando comando = mapper.Map<UsuarioCadastroComando>(request);
            Usuario response = await usuarioServico.CadastrarUsuarioAsync(comando, ct);
            return mapper.Map<UsuarioResponse>(response);
        }

        public async Task<PaginacaoConsulta<UsuarioResponse>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct)
        {
            UsuarioListarFiltro filtro = mapper.Map<UsuarioListarFiltro>(request);
            PaginacaoConsulta<Usuario> response = await usuariosRepositorio.ListarUsuariosAsync(filtro, ct);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(response);
        }
    }
}
