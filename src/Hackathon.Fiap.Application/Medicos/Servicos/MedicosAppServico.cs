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
    public class MedicosAppServico(IMapper mapper, IMedicosRepositorio medicosRepositorio) : IMedicosAppServico
    {
        public async Task<PaginacaoConsulta<MedicoResponse>> ListarMedicosComPaginacaoAsync(MedicosPaginacaoRequest request, CancellationToken ct)
        {
            MedicosPaginacaoFiltro contatosFiltro = mapper.Map<MedicosPaginacaoFiltro>(request);

            PaginacaoConsulta<Medico> consulta = await medicosRepositorio.ListarMedicosPaginadosAsync(contatosFiltro, ct);

            PaginacaoConsulta<MedicoResponse> response = mapper.Map<PaginacaoConsulta<MedicoResponse>>(consulta);

            return response;
        }
    }
}