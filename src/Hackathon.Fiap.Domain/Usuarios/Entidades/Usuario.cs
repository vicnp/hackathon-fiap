using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Domain.Usuarios.Entidades
{
    public class Usuario
    {
        public int UsuarioId { get; protected set; }
        public string Nome { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
        public string Cpf { get; protected set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; protected set; } = new TipoUsuario();
        public DateTime CriadoEm { get; protected set; } = new DateTime();

        public Usuario()
        {

        }

        public Usuario(int id, string nome, string email, string cpf, string senhaHash, TipoUsuario tipo)
        {
            UsuarioId = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Hash = senhaHash;
            Tipo = tipo;
            CriadoEm = DateTime.Now;
        }

        public Usuario(string nome, string email, string cpf, string senhaHash, TipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Hash = senhaHash;
            Tipo = tipo;
            CriadoEm = DateTime.Now;
        }

        public void AtualizarNome(string novoNome)
        {
            Nome = novoNome;
        }

        public void AtualizarEmail(string novoEmail)
        {
            Email = novoEmail;
        }

        public void AtualizarSenha(string novaSenhaHash)
        {
            Hash = novaSenhaHash;
        }
    }
}
