using AutoMapper;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.Application.Pacientes.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Application.Pacientes.Servicos
{
    public class PacientesAppServico(IMapper mapper, IPacientesRepositorio pacientesRepositorio, ISessaoServico sessaoServico) : IPacientesAppServico
    {
        public PaginacaoConsulta<PacienteResponse> ListarPacientes(UsuarioListarRequest request)
        {
            TipoUsuario? exemploRole = sessaoServico.RecuperarRoleUsuario();
            int? exemploId = sessaoServico.RecuperarCodigoUsuario();

            PaginacaoConsulta<Paciente> response = pacientesRepositorio.ListarPacientes(request);
            return mapper.Map<PaginacaoConsulta<PacienteResponse>>(response);
        }
    }
}