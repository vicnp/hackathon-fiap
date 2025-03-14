using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros
{
    public class MedicosPaginacaoFiltro : PaginacaoFiltro
    {
        public MedicosPaginacaoFiltro() : base("nome", TipoOrdernacao.Desc)
        {
        }

        public int? CodigoEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
    }
}