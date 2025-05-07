using AutoMapper;
using Hackathon.Fiap.Application.Medicos.Interfaces;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Medicos.Servicos
{
    public class MedicosAppServico(IMapper mapper, IMedicosRepositorio medicosRepositorio, IEspecialidadesRepositorio especialidadesRepositorio) : IMedicosAppServico
    {
        public async Task<PaginacaoConsulta<MedicoResponse>> ListarMedicosComPaginacaoAsync(MedicosPaginacaoRequest request, CancellationToken ct)
        {
            MedicosPaginacaoFiltro contatosFiltro = mapper.Map<MedicosPaginacaoFiltro>(request);

            PaginacaoConsulta<Medico> medicos = await medicosRepositorio.ListarMedicosPaginadosAsync(contatosFiltro, ct);
            foreach (var medico in medicos.Registros)
            {
                var especialidades = await especialidadesRepositorio.ListarEspecialidadesMedicoAsync(medico.UsuarioId, ct);
                foreach (var especialidade in especialidades)
                    medico.SetEspecialidade(especialidade);
            }

            PaginacaoConsulta<MedicoResponse> response = mapper.Map<PaginacaoConsulta<MedicoResponse>>(medicos);

            return response;
        }
    }
}