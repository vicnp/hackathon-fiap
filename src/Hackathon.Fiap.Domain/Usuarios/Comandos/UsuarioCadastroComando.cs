using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Domain.Usuarios.Comandos
{
    public class UsuarioCadastroComando
    {
        public string Nome { get; set; } = string.Empty;
        public string SobreNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public TipoUsuario TipoUsuario { get; set; }
        public string Senha { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
    }
}
