namespace Hackathon.Fiap.DataTransfer.Medicos.Responses
{
    public class MedicoResponse
    {
        public int IdUsuario { get; protected set; }
        public string Nome { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
        public string Cpf { get; protected set; } = string.Empty;
        public string Tipo { get; protected set; } = string.Empty;
        public string Crm { get; protected set; } = string.Empty;
        public EspecialidadeResponse? Especialidade { get; protected set; }
    }
}