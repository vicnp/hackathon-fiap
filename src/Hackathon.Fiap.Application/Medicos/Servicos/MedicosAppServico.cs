using AutoMapper;
using Hackathon.Fiap.Application.Medicos.Interfaces;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;

namespace Hackathon.Fiap.Application.Medicos.Servicos
{
    public class MedicosAppServico(IMapper mapper, IMedicosRepositorio medicosRepositorio) : IMedicosAppServico
    {
        public async Task<PaginacaoConsulta<MedicoResponse>> ListarMedicosComPaginacaoAsync(MedicosPaginacaoRequest request)
        {
            MedicosPaginacaoFiltro contatosFiltro = mapper.Map<MedicosPaginacaoFiltro>(request);

            PaginacaoConsulta<Medico> consulta = await medicosRepositorio.ListarMedicosPaginadosAsync(contatosFiltro);

            PaginacaoConsulta<MedicoResponse> response = mapper.Map<PaginacaoConsulta<MedicoResponse>>(consulta);

            return response;
        }
    }
}