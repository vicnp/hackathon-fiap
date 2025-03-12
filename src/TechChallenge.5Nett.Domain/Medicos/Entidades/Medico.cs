using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Entidades;

namespace Medicos.Entidades
{
    public class Medico : Usuario
    {
        public string Crm { get; protected set; } = "";
        public Especialidade Especialidade { get; protected set; }
        public Medico() { }

        public void SetCrm(string crm)
        {
            Crm = crm;
        }

        public void SetEspecialidade(Especialidade especialidade)
        {
            ArgumentNullException.ThrowIfNull(especialidade);

            Especialidade = especialidade;
        }
    }
}