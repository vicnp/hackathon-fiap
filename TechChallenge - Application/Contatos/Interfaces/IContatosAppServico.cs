using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Utils;
using YCTC_DataTransfer.Contatos.Requests;

namespace TC_Application.Contatos.Interfaces
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
