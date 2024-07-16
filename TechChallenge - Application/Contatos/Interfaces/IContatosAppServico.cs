using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Utils;
using YCTC_DataTransfer.Contatos.Requests;

namespace TC_Application.Contatos.Interfaces
{
    public interface IContatosAppServico
    {
        ContatoResponse? AtualizarContato(ContatoCrudRequest request, int id);
        ContatoResponse InserirContato(ContatoCrudRequest request);
        PaginacaoConsulta<ContatoResponse> ListarContatosComPaginacao(ContatoPaginacaoRequest request);
        List<ContatoResponse> ListarContatosSemPaginacao(ContatoRequest request);
        void RemoverContato(int id);
    }
}
