namespace Hackathon.Fiap.Domain.Medicos.Entidades
{
    public class Especialidade
    {
        public Especialidade(string nomeEspecialidade, string descricaoEspecialidade)
        {
            NomeEspecialidade = nomeEspecialidade;
            DescricaoEspecialidade = descricaoEspecialidade;
        }
        public Especialidade()
        {

        }
        public int IdEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string DescricaoEspecialidade { get; set; } = string.Empty;

    }
}