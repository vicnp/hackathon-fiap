using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces
{
    public interface ISessaoServico
    {
        int? RecuperarIdUsuario();
        TipoUsuario? RecuperarRoleUsuario();
    }
}