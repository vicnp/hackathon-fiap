using System.Net.Mail;
using System.Text.RegularExpressions;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Usuarios.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Domain.Usuarios.Servicos
{
    public partial class UsuariosServico(IUsuariosRepositorio usuariosRepositorio, ITokenServico tokenServico) : IUsuariosServico
    {
        private const int TAMANHO_MINIMO_NOME = 2;
        private const int TAMANHO_MINIMO_EMAIL = 4;
        private const int TAMANHO_MINIMO_SENHA = 4;
        [GeneratedRegex(@"^\d{4,7}(\/[A-Z]{2})?$")]
        private static partial Regex RegexCRM();
        public Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroComando comando, CancellationToken ct)
        {
            ValidarInformacoesUsuario(comando);
            string hash = GerarHashSenha(comando.Senha);

            if (comando.TipoUsuario == Enumeradores.TipoUsuario.Medico)
            {
                Medico novoMedico = new(comando.Nome + " " + comando.SobreNome, comando.Email, comando.Cpf, comando.Crm, hash, comando.TipoUsuario);
                return usuariosRepositorio.InserirUsuarioAsync(novoMedico, ct);
            }

            Usuario novoUsuario = new(comando.Nome + " " + comando.SobreNome, comando.Email, comando.Cpf, hash, comando.TipoUsuario);
            return usuariosRepositorio.InserirUsuarioAsync(novoUsuario, ct);
        }

        private static void ValidarInformacoesUsuario(UsuarioCadastroComando comando)
        {
            if (comando.TipoUsuario == Enumeradores.TipoUsuario.Medico && !ValidarCrm(comando.Crm))
                throw new RegraDeNegocioExcecao("Para o cadastro de médicos é obrigatório informar um código de CRM válido.");

            if (string.IsNullOrEmpty(comando.Senha) || comando.Senha.Length < TAMANHO_MINIMO_SENHA)
                throw new RegraDeNegocioExcecao("Informe uma senha válida.");

            if (string.IsNullOrEmpty(comando.Nome) || comando.Nome.Length < TAMANHO_MINIMO_NOME)
                throw new RegraDeNegocioExcecao("Informe um nome válido");

            if (string.IsNullOrEmpty(comando.SobreNome) || comando.SobreNome.Length < TAMANHO_MINIMO_NOME)
                throw new RegraDeNegocioExcecao("Informe um sobre nome válido");

            if (!ValidarEmail(comando.Email) || comando.Email.Length < TAMANHO_MINIMO_EMAIL)
                throw new RegraDeNegocioExcecao("Informe um Email válido");

            if (!ValidarCpf(comando.Cpf))
                throw new RegraDeNegocioExcecao("Informe um CPF válido");

        }

        public static bool ValidarCpf(string cpf)
        {
            const int CPF_SIZE = 11;

            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = new string([.. cpf.Where(char.IsDigit)]);

            if (cpf.Length != CPF_SIZE || cpf.Distinct().Count() == 1)
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * (CPF_SIZE - 1 - i);

            int primeiroDigito = sum % CPF_SIZE;
            primeiroDigito = primeiroDigito < 2 ? 0 : CPF_SIZE - primeiroDigito;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (cpf[i] - '0') * (CPF_SIZE - i);

            int segundoDigito = sum % CPF_SIZE;
            segundoDigito = segundoDigito < 2 ? 0 : CPF_SIZE - segundoDigito;

            return cpf[9] - '0' == primeiroDigito && cpf[CPF_SIZE - 1] - '0' == segundoDigito;
        }

        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                MailAddress addr = new(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GerarHashSenha(string senha)
        {
            return tokenServico.EncryptPassword(senha);
        }

        public static bool ValidarCrm(string crm)
        {
            if (string.IsNullOrWhiteSpace(crm))
                return false;

            crm = crm.Trim().ToUpper();

            Regex regex = RegexCRM();
            return regex.IsMatch(crm);
        }
    }
}
