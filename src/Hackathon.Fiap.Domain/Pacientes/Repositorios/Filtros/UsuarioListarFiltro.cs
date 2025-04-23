using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros
{
    public class UsuarioListarFiltro : PaginacaoFiltro
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Id { get; set; } 
        public string Cpf { get; set; } = string.Empty;
        public UsuarioListarFiltro() : base("nome", TipoOrdernacao.Desc)
        {
        }
    }
}
