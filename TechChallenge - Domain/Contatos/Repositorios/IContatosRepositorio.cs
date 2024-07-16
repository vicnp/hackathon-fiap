using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Utils;

namespace TC_Domain.Contatos.Repositorios
{
    public interface IContatosRepositorio
    {
        Contato AtualizarContato(Contato contato);
        Contato InserirContato(Contato contato);
        List<Contato> ListarContatos(ContatoFiltro filtro);
        PaginacaoConsulta<Contato> ListarPaginacaoContatos(ContatosPaginadosFiltro filtro);
        Contato RecuperarContato(int id);
        void RemoverContato(int id);
    }
}
