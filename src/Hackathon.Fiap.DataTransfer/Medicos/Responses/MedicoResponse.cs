namespace Hackathon.Fiap.DataTransfer.Medicos.Responses
{
    public class MedicoResponse
    {
        public int UsuarioId { get; protected set; }
        public string Nome { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
        public string Cpf { get; protected set; } = string.Empty;
        public string Tipo { get; protected set; } = string.Empty;
        public string Crm { get; protected set; } = string.Empty;
        public List<EspecialidadeResponse> Especialidades { get; protected set; } = [];
    }
}