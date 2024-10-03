using AutoMapper;
using Contatos.Requests;
using Utils;
using Regioes.Responses;
using Contatos.Reponses;
using Regioes.Repositorios;
using Contatos.Repositorios.Filtros;
using Regioes.Entidades;
using Contatos.Servicos.Interfaces;
using Contatos.Entidades;
using Contatos.Interfaces;

namespace Contatos.Servicos
{
    public class ContatosAppServico(IContatosServico contatosServico, IRegioesRepositorio regioesRepositorio, IMapper mapper) : IContatosAppServico
    {
        public async Task<PaginacaoConsulta<ContatoResponse>> ListarContatosComPaginacaoAsync(ContatoPaginacaoRequest request)
        {
            ContatosPaginadosFiltro contatosFiltro = mapper.Map<ContatosPaginadosFiltro>(request);

            PaginacaoConsulta<Contato> consulta = await contatosServico.ListarPaginacaoContatosAsync(contatosFiltro);

            PaginacaoConsulta<ContatoResponse> response = mapper.Map<PaginacaoConsulta<ContatoResponse>>(consulta);

            return response;
        }

        public async Task<ContatoResponse> InserirContatoAsync(ContatoCrudRequest request)
        {
            ContatoFiltro contatoFiltro = mapper.Map<ContatoFiltro>(request);

            Contato contato = await contatosServico.InserirContatoAsync(contatoFiltro);

            ContatoResponse contatoResponse = mapper.Map<ContatoResponse>(contato);

            List<Regiao> result = await regioesRepositorio.ListarRegioesAsync((int)contatoResponse.DDD!);
            Regiao? regiaoConsulta = result.FirstOrDefault();

            RegiaoResponse regiaoResponse = mapper.Map<RegiaoResponse>(regiaoConsulta);

            contatoResponse.Regiao = regiaoResponse;
            return contatoResponse;
        }

        public async Task<ContatoResponse?> AtualizarContatoAsync(ContatoCrudRequest request, int id)
        {
            if (!request.DDD.HasValue || request.DDD <= 0)
                throw new Exception("Código de DDD inválido.");
            List<Regiao> result = await regioesRepositorio.ListarRegioesAsync((int)request.DDD!);
            if (result.Count == 0)
                throw new Exception("Região não encontrada.");

            Contato contatoAtualizado = await contatosServico.RecuperarContatoAsync(id) ?? throw new Exception("Usuário não encontrado.");

            contatoAtualizado.SetDDD((int)request.DDD!);
            contatoAtualizado.SetEmail(request.Email!);
            contatoAtualizado.SetNome(request.Nome!);
            contatoAtualizado.SetTelefone(request.Telefone!);

            return mapper.Map<ContatoResponse>(await contatosServico.AtualizarContatoAsync(contatoAtualizado));
        }

        public async Task RemoverContatoAsync(int id)
        {
            await contatosServico.RemoverContatoAsync(id);
        }

        public async Task<List<ContatoResponse>> ListarContatosSemPaginacaoAsync(ContatoRequest request)
        {
            ContatoFiltro contatosFiltro = mapper.Map<ContatoFiltro>(request);

            List<Contato> consulta = await contatosServico.ListarContatosAsync(contatosFiltro);

            List<ContatoResponse> response = mapper.Map<List<ContatoResponse>>(consulta);

            return response;
        }

        public async Task<ContatoResponse> RecuperarContatoAsync(int id)
        {
            Contato consulta = await contatosServico.RecuperarContatoAsync(id) ?? throw new Exception("Registro não encontrado!");

            List<Regiao> regiao = await regioesRepositorio.ListarRegioesAsync((int)consulta.DDD!);

            consulta.SetRegiao(regiao.FirstOrDefault());
            return mapper.Map<ContatoResponse>(consulta);
        }
    }
}
