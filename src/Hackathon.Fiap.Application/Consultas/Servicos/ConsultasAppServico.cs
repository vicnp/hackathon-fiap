using AutoMapper;
using Hackathon.Fiap.Application.Consultas.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;

namespace Hackathon.Fiap.Application.Consultas.Servicos
{
    public class ConsultasAppServico(IMapper mapper, IConsultaServico consultasServico) : IConsultasAppServico
    {
        public async Task<PaginacaoConsulta<ConsultaResponse>> ListarConsultasAsync(ConsultaListarRequest request, CancellationToken ct)
        {
            ConsultasListarFiltro filtro = mapper.Map<ConsultasListarFiltro>(request);

            PaginacaoConsulta<Consulta> consulta = await consultasServico.ListarConsultasAsync(filtro, ct);

            PaginacaoConsulta<ConsultaResponse> response = mapper.Map<PaginacaoConsulta<ConsultaResponse>>(consulta);
            return response;
        }
    }
}
