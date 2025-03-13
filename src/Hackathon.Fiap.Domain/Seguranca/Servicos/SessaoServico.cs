using System.Security.Claims;
using Seguranca.Servicos.Interfaces;
using Usuarios.Enumeradores;
using Microsoft.AspNetCore.Http;

namespace Seguranca.Servicos
{
    public class SessaoServico(IHttpContextAccessor httpContextAccessor) : ISessaoServico
    {
        public TipoUsuario? RecuperarRoleUsuario()
        {
            string? value = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            if (value == null)
                return null;

            return value switch
            {
                "Medico" => (TipoUsuario?)TipoUsuario.Medico,
                "Paciente" => (TipoUsuario?)TipoUsuario.Paciente,
                _ => null,
            };
        }

        public int? RecuperarCodigoUsuario()
        {
            string? value = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;

            if(value == null)
                return null;

            return int.Parse(value);
        }
    }
}