using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medicos.Entidades;
using Medicos.Repositorios.Filtros;
using Utils;

namespace Medicos.Repositorios
{
    public interface IMedicosRepositorio
    {
        Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro);
    }
}