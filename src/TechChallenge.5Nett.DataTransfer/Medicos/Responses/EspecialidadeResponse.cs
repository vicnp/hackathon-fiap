using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicos.Responses
{
    public class EspecialidadeResponse
    {
        public int IdEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
        public string DescricaoEspecialidade { get; set; } = string.Empty;
    }
}