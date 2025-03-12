using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicos.Entidades
{
    public class Especialidade
    {
        public int IdEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string DescricaoEspecialidade { get; set;} = string.Empty;
    }
}