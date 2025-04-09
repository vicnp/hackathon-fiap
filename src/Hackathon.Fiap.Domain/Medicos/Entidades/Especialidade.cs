namespace Hackathon.Fiap.Domain.Medicos.Entidades
{
    public class Especialidade
    {
        public int EspecialidadeId { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string DescricaoEspecialidade { get; set; } = string.Empty;
    }
}