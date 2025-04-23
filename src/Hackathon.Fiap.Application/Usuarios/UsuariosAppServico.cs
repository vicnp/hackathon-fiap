using System.Text.RegularExpressions;
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
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Application.Usuarios
{
    public partial class UsuariosAppServico(IUsuariosRepositorio usuariosRepositorio, IUsuariosServico usuarioServico, IMapper mapper) : IUsuariosAppServico
    {
        [GeneratedRegex(@"Duplicate entry '.*' for key '(Usuarios|Medicos)\.(\w+)'")]
        private static partial Regex RegexErroMessage();

        public async Task<UsuarioResponse> CadastrarUsuarioAsync(UsuarioCadastroRequest request, CancellationToken ct)
        {
            try
            {
                UsuarioCadastroComando comando = mapper.Map<UsuarioCadastroComando>(request);
                Usuario response = await usuarioServico.CadastrarUsuarioAsync(comando, ct);
                return mapper.Map<UsuarioResponse>(response);
            }
            catch (Exception e)
            {
                throw new RegraDeNegocioExcecao(SanitizeErrorMessage(e.Message));
            }

        }
        public static string SanitizeErrorMessage(string errorMessage)
        {
            Regex regex = RegexErroMessage();
            Match match = regex.Match(errorMessage);
            const int INDEX_FIELD = 2;
            if (match.Success && match.Groups.Count >= INDEX_FIELD)
            {
                string field = match.Groups[INDEX_FIELD].Value ?? "documento";
                return $"Um usuário com esse {field} já existe.";
            }
            return errorMessage;
        }

        public async Task<PaginacaoConsulta<UsuarioResponse>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct)
        {
            UsuarioListarFiltro filtro = mapper.Map<UsuarioListarFiltro>(request);
            PaginacaoConsulta<Usuario> response = await usuariosRepositorio.ListarUsuariosAsync(filtro, ct);
            return mapper.Map<PaginacaoConsulta<UsuarioResponse>>(response);
        }


    }
}
