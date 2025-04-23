using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Usuarios.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Domain.Usuarios.Servicos
{
    public class UsuariosServico(IUsuariosRepositorio usuariosRepositorio, ITokenServico tokenServico) : IUsuariosServico
    {
        private const int TAMANHO_MINIMO_NOME = 2;
        private const int TAMANHO_MINIMO_EMAIL = 4;
        private const int TAMANHO_MINIMO_SENHA = 4;

        public Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroComando comando, CancellationToken ct)
        {
            ValidarInformacoesUsuario(comando);

            string hash = GerarHashSenha(comando.Senha);

            Usuario novoUsuario = new(comando.Nome + " " + comando.SobreNome, comando.Email, comando.Cpf, hash, comando.TipoUsuario);

            return usuariosRepositorio.InserirUsuarioAsync(novoUsuario, ct);
        }

        private static void ValidarInformacoesUsuario(UsuarioCadastroComando comando)
        {
            if (comando.TipoUsuario == Enumeradores.TipoUsuario.Medico && string.IsNullOrEmpty(comando.Crm))
                throw new RegraDeNegocioExcecao("Para o cadastro de médicos é obrigatório informar um código de CRM válido.");

            if (string.IsNullOrEmpty(comando.Senha))
                throw new RegraDeNegocioExcecao("Informe uma senha válida.");

            if (string.IsNullOrEmpty(comando.Nome) || comando.Nome.Length < TAMANHO_MINIMO_NOME)
                throw new RegraDeNegocioExcecao("Informe um nome válido");

            if (string.IsNullOrEmpty(comando.SobreNome) || comando.SobreNome.Length < TAMANHO_MINIMO_NOME)
                throw new RegraDeNegocioExcecao("Informe um sobre nome válido");

            if (string.IsNullOrEmpty(comando.Email) || comando.Email.Length < TAMANHO_MINIMO_EMAIL)
                throw new RegraDeNegocioExcecao("Informe um Email válido");

            if (comando.Senha.Length == TAMANHO_MINIMO_SENHA)
                throw new RegraDeNegocioExcecao("Informe uma senha válida");
        }

        private string GerarHashSenha(string senha)
        {
            return tokenServico.EncryptPassword(senha);
        }
    }
}
