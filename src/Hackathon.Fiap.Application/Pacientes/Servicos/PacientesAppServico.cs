using AutoMapper;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.Application.Pacientes.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;

namespace Hackathon.Fiap.Application.Pacientes.Servicos
{
    public class PacientesAppServico(IMapper mapper, IPacientesRepositorio pacientesRepositorio, ISessaoServico sessaoServico) : IPacientesAppServico
    {
        public async Task<PaginacaoConsulta<PacienteResponse>> ListarPacientesAsync(UsuarioListarRequest request, CancellationToken ct)
        {
            TipoUsuario? exemploRole = sessaoServico.RecuperarRoleUsuario();
            int? exemploId = sessaoServico.RecuperarCodigoUsuario();

            UsuarioListarFiltro filtro = mapper.Map<UsuarioListarFiltro>(request);

            PaginacaoConsulta<Paciente> response = await pacientesRepositorio.ListarPacientesAsync(filtro, ct);
            return mapper.Map<PaginacaoConsulta<PacienteResponse>>(response);
        }
    }
}