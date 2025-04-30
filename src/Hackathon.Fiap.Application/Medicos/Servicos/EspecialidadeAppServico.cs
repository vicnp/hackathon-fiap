using AutoMapper;
using Hackathon.Fiap.Application.Medicos.Interfaces;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Medicos.Servicos
{
    public class EspecialidadeAppServico(
        IMapper mapper,
        IEspecialidadesServico especialidadesServico,
        IEspecialidadesRepositorio especialidadesRepositorio) : IEspecialidadeAppServico
    {
        public async Task DeletarEspecialidadeAsync(int especialidadeId, CancellationToken ct)
        {
            Especialidade especialidade = await especialidadesServico.ValidarEspecialidadeAsync(especialidadeId, ct);
            await especialidadesRepositorio.DeletarEspecialidadeAsync(especialidade.EspecialidadeId, ct);
        }

        public async Task<EspecialidadeResponse> InserirEspecialidadeAsync(EspecialidadeRequest especialidade, CancellationToken ct)
        {
            Especialidade novaEspecialidade = new(especialidade.NomeEspecialidade, especialidade.DescricaoEspecialidade);
            await especialidadesRepositorio.InserirEspecialidadeAsync(novaEspecialidade, ct);
            return mapper.Map<EspecialidadeResponse>(novaEspecialidade);
        }

        public async Task<PaginacaoConsulta<EspecialidadeResponse>> ListarEspecialidadesMedicosPaginadosAsync(EspecialidadesPaginacaoRequest request, CancellationToken ct)
        {
            EspecialidadePaginacaoFiltro filtro = mapper.Map<EspecialidadePaginacaoFiltro>(request);
            var result = await especialidadesRepositorio.ListarEspecialidadesMedicosPaginadosAsync(filtro, ct);
            return mapper.Map<PaginacaoConsulta<EspecialidadeResponse>>(result);
        }

        public async Task<EspecialidadeResponse> RecuperarEspecialidadeAsync(int especialidadeId, CancellationToken ct)
        {
            var response = await especialidadesServico.ValidarEspecialidadeAsync(especialidadeId, ct);
            return mapper.Map<EspecialidadeResponse>(response);
        }
    }
}
