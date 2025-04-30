using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Medicos.Requests
{
    public class EspecialidadesPaginacaoRequest : PaginacaoFiltro
    {
        public EspecialidadesPaginacaoRequest() : base("NomeEspecialidade", TipoOrdernacao.Desc)
        {
        }

        public int? CodigoEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
    }
}