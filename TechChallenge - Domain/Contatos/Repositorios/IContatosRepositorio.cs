using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Utils;

namespace TC_Domain.Contatos.Repositorios
{
    public interface IContatosRepositorio
    {
        Contato InserirContato(Contato contato);
        PaginacaoConsulta<Contato> ListarContatos(ContatosFiltro filtro);
    }
}
