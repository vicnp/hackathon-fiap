using Utils;
using Utils.Enumeradores;

namespace Usuarios.Request
{
    public class PacienteListarRequest : PaginacaoFiltro
    {
        public string? NomeUsuario { get; set; }
        public string? Email { get; set; }

        public string? Cpf {  get; set; }
        public PacienteListarRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }
    }
}
