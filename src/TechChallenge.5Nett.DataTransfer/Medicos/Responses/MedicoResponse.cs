using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicos.Responses
{
    public class MedicoResponse
    {
        public int Id { get; protected set; } 
        public string Nome { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
        public string Cpf { get; protected set; } = string.Empty;
        public string Tipo { get; protected set; } = string.Empty;
        public string Crm { get; protected set; } = string.Empty;
        public EspecialidadeResponse? Especialidade { get; protected set; }
    }
}