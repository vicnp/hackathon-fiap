using AutoMapper;
using Pacientes.Interfaces;
using Utils;
using Pacientes.Repositorios;
using Pacientes.Entidades;
using Pacientes.Responses;
using Usuarios.Request;

namespace Pacientes.Servicos
{
    public class PacientesAppServico(IMapper mapper,IPacientesRepositorio pacientesRepositorio) : IPacientesAppServico
    {
        public PaginacaoConsulta<PacienteResponse> ListarPacientes(UsuarioListarRequest request)
        {
            PaginacaoConsulta<Paciente> response = pacientesRepositorio.ListarPacientes(request);
            return mapper.Map<PaginacaoConsulta<PacienteResponse>>(response);
        }
    }
}