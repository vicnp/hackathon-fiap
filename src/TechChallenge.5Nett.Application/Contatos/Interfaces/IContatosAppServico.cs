using Contatos.Requests;
using Utils;
using Contatos.Reponses;

namespace Contatos.Interfaces
{
    public interface IContatosAppServico
    {
        Task<ContatoResponse?> AtualizarContatoAsync(ContatoCrudRequest request, int id);
        Task<ContatoResponse> InserirContatoAsync(ContatoCrudRequest request);
        Task<PaginacaoConsulta<ContatoResponse>> ListarContatosComPaginacaoAsync(ContatoPaginacaoRequest request);
        Task<List<ContatoResponse>> ListarContatosSemPaginacaoAsync(ContatoRequest request);
        Task<ContatoResponse> RecuperarContatoAsync(int id);
        Task RemoverContatoAsync(int id);
    }
}
