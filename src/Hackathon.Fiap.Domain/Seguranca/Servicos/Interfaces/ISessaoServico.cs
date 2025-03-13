using Usuarios.Enumeradores;

namespace Seguranca.Servicos.Interfaces
{
    public interface ISessaoServico
    {
        int? RecuperarCodigoUsuario();
        TipoUsuario? RecuperarRoleUsuario();
    }
}