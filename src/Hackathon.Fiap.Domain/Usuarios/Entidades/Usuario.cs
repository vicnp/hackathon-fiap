using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Domain.Usuarios.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; protected set; }
        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Cpf { get; protected set; }
        public string Hash { get; set; }
        public TipoUsuario Tipo { get; protected set; }
        public DateTime CriadoEm { get; protected set; }

        public Usuario()
        {

        }

        public Usuario(int id, string nome, string email, string cpf, string senhaHash, TipoUsuario tipo)
        {
            IdUsuario = id;
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
