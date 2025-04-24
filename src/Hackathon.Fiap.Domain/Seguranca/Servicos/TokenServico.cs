using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Hackathon.Fiap.Domain.Utils.Helpers;
using Hackathon.Fiap.Domain.Utils.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon.Fiap.Domain.Seguranca.Servicos
{
    public class TokenServico(IConfiguration configuration, IUsuariosRepositorio usuariosRepositorio, IUtilRepositorio utilRepositorio) : ITokenServico
    {
        private const string autenticacaoFalha = "Usuário ou senha incorretos.";

        public async Task<string> GetTokenAsync(string identificador, string senha, CancellationToken ct)
        {

            if (identificador.IsNullOrEmpty() || senha.IsNullOrEmpty())
                throw new NaoAutorizadoExcecao(autenticacaoFalha);

            string hash = EncryptPassword(senha);

            Usuario? usuario = await usuariosRepositorio.RecuperarUsuarioAsync(identificador, hash, ct);

            NaoAutorizadoExcecao.LancarExcecaoSeNulo(usuario, autenticacaoFalha);

            JwtSecurityTokenHandler tokenHanlder = new JwtSecurityTokenHandler();
            string? configurationValue = utilRepositorio.GetValueConfigurationKeyJWT(configuration)
                ?? throw new NullReferenceException("GetValueConfigurationKeyJWT Retornou valor nulo.");
            byte[] chaveCriptografia = Encoding.ASCII.GetBytes(configurationValue);

            SecurityTokenDescriptor tokenProps = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Tipo.ToString()),
                    new Claim(ClaimTypes.Sid, usuario.UsuarioId.ToString())
                ]),

                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveCriptografia), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHanlder.CreateToken(tokenProps);
            return tokenHanlder.WriteToken(token);
        }
        public string EncryptPassword(string password)
        {
            using Aes aes = Aes.Create();
            string? configurationValue = utilRepositorio.GetValueConfigurationHash(configuration)
                ?? throw new NullReferenceException("GetValueConfigurationKeyJWT Retornou valor nulo.");
            aes.Key = Encoding.UTF8.GetBytes(configurationValue);
            aes.IV = new byte[16];
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
            {
                using StreamWriter sw = new(cs);
                sw.Write(password);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using Aes aes = Aes.Create();
            string? configurationValue = utilRepositorio.GetValueConfigurationKeyJWT(configuration)
                ?? throw new NullReferenceException("GetValueConfigurationKeyJWT Retornou valor nulo.");
            aes.Key = Encoding.UTF8.GetBytes(configurationValue);
            aes.IV = new byte[16];
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream ms = new(Convert.FromBase64String(encryptedPassword));
            using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new(cs);
            return sr.ReadToEnd();
        }
    }
}
