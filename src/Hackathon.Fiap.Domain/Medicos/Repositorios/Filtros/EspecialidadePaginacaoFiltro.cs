using Hackathon.Fiap.Domain.Utils.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros
{
    public class EspecialidadePaginacaoFiltro : PaginacaoFiltro
    {
        public EspecialidadePaginacaoFiltro() : base("NomeEspecialidade", TipoOrdernacao.Desc)
        {
        }

        public int? CodigoEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
    }
}