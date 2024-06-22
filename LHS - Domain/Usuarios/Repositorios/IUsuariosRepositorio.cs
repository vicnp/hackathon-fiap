using TC_DataTransfer.Usuarios.Request;
using TC_Domain.Usuarios.Entidades;
using TC_IOT.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        /// <summary>
        /// Listagem pagina de usuários registrados no banco de dados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Total de registros na base e lista de registros recuperados da página.  </T>></returns>
        PaginacaoConsulta<Usuario> ListarUsuarios(UsuarioListarRequest request);
    }
}
