using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
namespace Hackathon.Fiap.Domain.Medicos.Entidades
{
    public class Medico : Usuario
    {
        public string Crm { get; protected set; } = "";
        public IList<Especialidade> Especialidades { get; protected set; } = [];
        public Medico() { }

        public Medico(int id, string nome, string email, string cpf, string senhaHash, TipoUsuario tipo) : base(id, nome, email, cpf, senhaHash, tipo)
        {
        }

        public Medico(string nome, string email, string cpf, string crm, string senhaHash, TipoUsuario tipo) : base(nome, email, cpf, senhaHash, tipo)
        {
            SetCrm(crm);
        }

        public void SetCrm(string crm)
        {
            Crm = crm;
        }

        public void SetEspecialidade(Especialidade? especialidade)
        {
            ArgumentNullException.ThrowIfNull(especialidade);

            Especialidades.Add(especialidade);
        }
    }
}