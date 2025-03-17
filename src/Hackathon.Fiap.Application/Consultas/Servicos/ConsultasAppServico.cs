using AutoMapper;
using Hackathon.Fiap.Application.Consultas.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
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

        public async Task<ConsultaResponse> AlterarStatusConsultaAsync(ConsultaStatusRequest request, string justificativa, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);
            Consulta consulta = await ValidarConsulta(request, ct);
            consulta.JustificativaCancelamento = justificativa;
            Consulta? consultaResponse = await consultasServico.AtualizarStatusConsultaAsync(consulta, request.Status, ct);

            return mapper.Map<ConsultaResponse>(consultaResponse);
        }
       
        private async Task<Consulta> ValidarConsulta(ConsultaStatusRequest request, CancellationToken ct)
        {
            ConsultasListarFiltro filtro = new()
            {
                IdConsulta = request.IdConsulta,
            };
            PaginacaoConsulta<Consulta> consultas = await consultasServico.ListarConsultasAsync(filtro, ct);

            ArgumentNullException.ThrowIfNull(consultas);

            if (!consultas.Registros.Any())
                throw new ArgumentException("Consulta não encontrada!");

            return consultas.Registros.First();
        }
    }
}
