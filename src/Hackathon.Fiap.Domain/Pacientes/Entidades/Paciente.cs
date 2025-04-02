using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Domain.Pacientes.Entidades
{
    public class Paciente : Usuario
    {
        public Paciente(){}
        
        public Paciente(int id, string nome, string email, string cpf, string senhaHash, TipoUsuario tipo)
            : base(id, nome, email, cpf, senhaHash, tipo)
        {
        }
    }
}