using LHS_DataTransfer.Usuarios.Request;
using LHS_DataTransfer.Usuarios.Response;
using LHS_Domain.Usuarios.Entidades;
using LHS_IOT.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHS_Application.Usuarios.Interfaces
{
    public interface IUsuariosAppServico
    {
        PaginacaoConsulta<UsuarioResponse> ListarUsuarios(UsuarioListarRequest request);
    }
}
