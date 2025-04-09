using Hackathon.Fiap.Domain.Usuarios.Entidades;

namespace Hackathon.Fiap.Domain.Medicos.Entidades
{
    public class Medico : Usuario
    {
        public string Crm { get; protected set; } = "";
        public Especialidade Especialidade { get; protected set; } = new Especialidade();
        public Medico() { }

        public void SetCrm(string crm)
        {
            Crm = crm;
        }

        public void SetEspecialidade(Especialidade? especialidade)
        {
            ArgumentNullException.ThrowIfNull(especialidade);

            Especialidade = especialidade;
        }
    }
}