using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Enumeradores;

namespace Medicos.Repositorios.Filtros
{
    public class MedicosPaginacaoFiltro : PaginacaoFiltro
    {
        public MedicosPaginacaoFiltro() : base("nome", TipoOrdernacao.Desc)
        {
        }

        public int? CodigoEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string Nome {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
    }
}