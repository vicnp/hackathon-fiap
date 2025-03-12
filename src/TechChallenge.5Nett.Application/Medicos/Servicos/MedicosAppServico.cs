using AutoMapper;
using Contatos.Requests;
using Medicos.Entidades;
using Medicos.Interfaces;
using Medicos.Repositorios;
using Medicos.Repositorios.Filtros;
using Medicos.Requests;
using Medicos.Responses;
using Utils;

namespace Medicos.Servicos
{
    public class MedicosAppServico(IMapper mapper, IMedicosRepositorio medicosRepositorio) : IMedicosAppServico
    {
        public async Task<PaginacaoConsulta<MedicoResponse>> ListarContatosComPaginacaoAsync(MedicosPaginacaoRequest request)
        {
            MedicosPaginacaoFiltro contatosFiltro = mapper.Map<MedicosPaginacaoFiltro>(request);

            PaginacaoConsulta<Medico> consulta = await medicosRepositorio.ListarMedicosPaginadosAsync(contatosFiltro);

            PaginacaoConsulta<MedicoResponse> response = mapper.Map<PaginacaoConsulta<MedicoResponse>>(consulta);

            return response;
        }
    }
}