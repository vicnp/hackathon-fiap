using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Utils;

namespace TC_Application.Contatos.Interfaces
{
    public interface IContatosAppServico
    {
        ContatoResponse InserirContato(ContatoInserirRequest request);
        PaginacaoConsulta<ContatoResponse> ListarContatosComPaginacao(ContatoRequest request);
    }
}
