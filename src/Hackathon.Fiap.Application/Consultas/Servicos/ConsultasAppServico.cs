using AutoMapper;
using Hackathon.Fiap.Application.Consultas.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Consultas.Servicos
{
    public class ConsultasAppServico(IMapper mapper, IConsultasServico consultasServico) : IConsultasAppServico
    {
        public async Task<PaginacaoConsulta<ConsultaResponse>> ListarConsultasAsync(ConsultaListarRequest request, CancellationToken ct)
        {
            ConsultasListarFiltro filtro = mapper.Map<ConsultasListarFiltro>(request);

            PaginacaoConsulta<Consulta> consulta = await consultasServico.ListarConsultasAsync(filtro, ct);

            PaginacaoConsulta<ConsultaResponse> response = mapper.Map<PaginacaoConsulta<ConsultaResponse>>(consulta);
            return response;
        }

        public async Task<ConsultaResponse> AlterarStatusConsultaAsync(ConsultaStatusRequest request, string? justificativa, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);
            Consulta consulta = await ValidarConsulta(request.ConsultaId, ct);
            consulta.JustificativaCancelamento = justificativa!;
            Consulta? consultaResponse = await consultasServico.AtualizarStatusConsultaAsync(consulta, request.Status, ct);

            return mapper.Map<ConsultaResponse>(consultaResponse);
        }

        public async Task<ConsultaResponse> InserirConsultaAsync(ConsultaRequest request, CancellationToken ct)
        {
            ConsultaInserirFiltro consultaInserirFiltro = mapper.Map<ConsultaInserirFiltro>(request);
            return mapper.Map<ConsultaResponse>(await consultasServico.InserirConsultaAsync(consultaInserirFiltro, ct));
        }


        private async Task<Consulta> ValidarConsulta(int consultaId, CancellationToken ct)
        {
            ConsultasListarFiltro filtro = new()
            {
                ConsultaId = consultaId,
            };
            PaginacaoConsulta<Consulta> consultas = await consultasServico.ListarConsultasAsync(filtro, ct);

            ArgumentNullException.ThrowIfNull(consultas);

            if (!consultas.Registros.Any())
                throw new ArgumentException("Consulta não encontrada!");

            return consultas.Registros.First();
        }
    }
}
