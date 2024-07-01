using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Utils;

namespace TC_Domain.Contatos.Servicos.Interfaces
{
    public interface IContatosServico
    {
        Contato InserirContato(ContatoInserirRequest contato);
        PaginacaoConsulta<Contato> ListarContatos(ContatosFiltro request);
    }
}
