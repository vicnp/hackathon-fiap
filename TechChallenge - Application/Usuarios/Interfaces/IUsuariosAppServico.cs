using TC_DataTransfer.Usuarios.Request;
using TC_DataTransfer.Usuarios.Response;
using TC_Domain.Usuarios.Entidades;
using TC_IOC.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Application.Usuarios.Interfaces
{
    public interface IUsuariosAppServico
    {
        PaginacaoConsulta<UsuarioResponse> ListarUsuarios(UsuarioListarRequest request);
    }
}
