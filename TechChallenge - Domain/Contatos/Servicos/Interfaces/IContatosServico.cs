using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Utils;

namespace TC_Domain.Contatos.Servicos.Interfaces
{
    public interface IContatosServico
    {
        Task<Contato> AtualizarContatoAsync(Contato contato);
        Task<Contato> InserirContatoAsync(ContatoFiltro contato);
        Task<List<Contato>> ListarContatosAsync(ContatoFiltro request);
        Task<PaginacaoConsulta<Contato>> ListarPaginacaoContatosAsync(ContatosPaginadosFiltro request);
        Task<Contato> RecuperarContatoAsync(int id);
        Task RemoverContatoAsync(int id);
    }
}
