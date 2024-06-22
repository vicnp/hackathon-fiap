using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.Usuarios.Entidades
{
    public class Usuario
    {
        public int CodigoUsuario { get; set; }
        public string Nome { get; set;}
        public string Email { get; set;}
        public string CodigoCargo { get; set;}

        public Usuario()
        {
            
        }
    }
}
