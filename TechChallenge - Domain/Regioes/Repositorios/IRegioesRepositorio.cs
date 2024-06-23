using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Regioes.Repositorios.Consultas;

namespace TC_Domain.Regioes.Repositorios
{
    public interface IRegioesRepositorio
    {
        List<RegiaoConsulta> ListarRegioes(int ddd = 0);
    }
}
