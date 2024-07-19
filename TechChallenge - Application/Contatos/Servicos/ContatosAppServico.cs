using AutoMapper;
using TC_Application.Contatos.Interfaces;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Regioes.Repositorios;
using TC_DataTransfer.Regiao.Responses;
using TC_Domain.Utils;
using TC_Domain.Regioes.Entidades;
using YCTC_DataTransfer.Contatos.Requests;

namespace TC_Application.Contatos.Servicos
{
    public class ContatosAppServico(IContatosServico contatosServico,IRegioesRepositorio regioesRepositorio, IMapper mapper) : IContatosAppServico
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
            if(!request.DDD.HasValue || request.DDD <= 0)
                throw new Exception("Código de DDD inválido.");
            List<Regiao> result = await regioesRepositorio.ListarRegioesAsync((int)request.DDD!);
            if(result.Count == 0)
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
            Contato consulta = await contatosServico.RecuperarContatoAsync(id);
            return mapper.Map<ContatoResponse>(consulta);
        }
    }
}
