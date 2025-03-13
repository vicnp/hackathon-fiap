using AutoMapper;
using Pacientes.Interfaces;
using Utils;
using Pacientes.Repositorios;
using Pacientes.Entidades;
using Pacientes.Responses;
using Usuarios.Request;
using Seguranca.Servicos.Interfaces;
using Usuarios.Enumeradores;

namespace Pacientes.Servicos
{
    public class PacientesAppServico(IMapper mapper,IPacientesRepositorio pacientesRepositorio, ISessaoServico sessaoServico) : IPacientesAppServico
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