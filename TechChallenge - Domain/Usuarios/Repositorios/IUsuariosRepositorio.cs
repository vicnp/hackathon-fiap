using TC_DataTransfer.Usuarios.Request;
using TC_Domain.Usuarios.Entidades;
using TC_IOC.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        PaginacaoConsulta<Usuario> ListarUsuarios(UsuarioListarRequest request);
        Usuario RecuperarUsuario(string email, string hash);
    }
}
