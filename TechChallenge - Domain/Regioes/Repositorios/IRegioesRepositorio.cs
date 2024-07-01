using TC_Domain.Regioes.Entidades;

namespace TC_Domain.Regioes.Repositorios
{
    public interface IRegioesRepositorio
    {
        List<Regiao> ListarRegioes(int ddd = 0);
    }
}
