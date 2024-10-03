using Utils;
using Utils.Enumeradores;

namespace Usuarios.Request
{
    public class UsuarioListarRequest : PaginacaoFiltro
    {
        public string? NomeUsuario { get; set; }
        public string? Email { get; set; }
        public UsuarioListarRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }
    }
}
