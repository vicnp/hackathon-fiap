using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Usuarios.Request
{
    public class UsuarioListarRequest : PaginacaoFiltro
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public UsuarioListarRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }
    }
}
