using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Utils;

namespace TC_Domain.Contatos.Servicos.Interfaces
{
    public interface IContatosServico
    {
        Contato AtualizarContato(Contato contato);
        Contato InserirContato(ContatoFiltro contato);
        PaginacaoConsulta<Contato> ListarContatos(ContatosPaginadosFiltro request);
        Contato RecuperarContato(int id);
        void RemoverContato(int id);
    }
}
