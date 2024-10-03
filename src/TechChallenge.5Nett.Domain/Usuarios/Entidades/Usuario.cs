namespace Usuarios.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Permissao { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string nome, string hash, string email, int permissao)
        {
            Id = id;
            Nome = nome;
            Hash = hash;
            Email = email;
            DataCriacao = DateTime.Now;
            Permissao = permissao;
        }
    }
}
