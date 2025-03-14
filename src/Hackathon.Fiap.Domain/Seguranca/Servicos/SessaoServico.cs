using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Domain.Seguranca.Servicos
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

            if (value == null)
                return null;

            return int.Parse(value);
        }
    }
}