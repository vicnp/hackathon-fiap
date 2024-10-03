using Regioes.Entidades;

namespace Regioes.Repositorios
{
    public interface IRegioesRepositorio
    {
        Task<List<Regiao>> ListarRegioesAsync(int ddd = 0);
    }
}
