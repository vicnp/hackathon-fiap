using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.Contatos.Entidades
{
    public class Contato
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? DDD { get; set; }
        public string? Telefone { get; set; }

        public Contato()
        {
            
        }
    }
}
