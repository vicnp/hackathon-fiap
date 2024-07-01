using TC_Domain.Utils;
using TC_Domain.Utils.Enumeradores;

namespace TC_DataTransfer.Usuarios.Request
{
    public class UsuarioListarRequest : PaginacaoFiltro
    {
        public string? NomeUsuario { get; set; }
        public string? Email {  get; set; }
        public UsuarioListarRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }
    }
}
