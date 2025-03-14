using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Medicos.Requests
{
    public class MedicosPaginacaoRequest : PaginacaoFiltro
    {
        public MedicosPaginacaoRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }

        public int CodigoEspecialidade { get; set; } = 0;
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
    }
}