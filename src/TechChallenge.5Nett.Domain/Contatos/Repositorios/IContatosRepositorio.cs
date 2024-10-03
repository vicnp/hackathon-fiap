using Contatos.Entidades;
using Contatos.Repositorios.Filtros;
using Utils;

namespace Contatos.Repositorios
{
    public interface IContatosRepositorio
    {
        Task<Contato> AtualizarContatoAsync(Contato contato);
        Task<Contato> InserirContatoAsync(Contato contato);
        Task<List<Contato>> ListarContatosAsync(ContatoFiltro filtro);
        Task<PaginacaoConsulta<Contato>> ListarPaginacaoContatosAsync(ContatosPaginadosFiltro filtro);
        Task<Contato> RecuperarContatoAsync(int id);
        Task RemoverContatoAsync(int id);
    }
}
