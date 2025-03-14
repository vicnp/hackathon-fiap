namespace Hackathon.Fiap.DataTransfer.Usuarios.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }

        public UsuarioResponse()
        {

        }
    }
}
