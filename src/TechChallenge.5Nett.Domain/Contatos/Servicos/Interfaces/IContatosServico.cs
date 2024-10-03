using Contatos.Entidades;
using Contatos.Repositorios.Filtros;
using Utils;

namespace Contatos.Servicos.Interfaces
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
