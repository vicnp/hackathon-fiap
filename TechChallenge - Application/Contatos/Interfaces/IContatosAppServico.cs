using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;

namespace TC_Application.Contatos.Interfaces
{
    public interface IContatosAppServico
    {
        PaginacaoConsulta<ContatoResponse> ListarContatosComPaginacao(ContatoRequest request);
    }
}
