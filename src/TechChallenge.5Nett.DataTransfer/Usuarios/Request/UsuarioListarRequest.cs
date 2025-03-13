using Utils;
using Utils.Enumeradores;

namespace Usuarios.Request
{
    public class UsuarioListarRequest : PaginacaoFiltro
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;
        public UsuarioListarRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }
    }
}
