using TC_Domain.Regioes.Entidades;

namespace TC_Domain.Regioes.Repositorios
{
    public interface IRegioesRepositorio
    {
        Task<List<Regiao>> ListarRegioesAsync(int ddd = 0);
    }
}
