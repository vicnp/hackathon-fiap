using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Seguranca.Servicos.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Usuarios.Entidades;
using Usuarios.Repositorios;
using Utils.Repositorios;

namespace Seguranca.Servicos
{
    public class TokenServico(IConfiguration configuration, IUsuariosRepositorio usuariosRepositorio, IUtilRepositorio utilRepositorio) : ITokenServico
    {
        public string GetToken(string email, string senha)
        {
            var hash = EncryptPassword(senha);

            Usuario usuario = usuariosRepositorio.RecuperarUsuario(email, hash);

            if (usuario == null)
                return string.Empty;

            var tokenHanlder = new JwtSecurityTokenHandler();
            var chaveCriptografia = Encoding.ASCII.GetBytes(utilRepositorio.GetValueConfigurationKeyJWT(configuration));

            var tokenProps = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Permissao.ToString())
                ]),

                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveCriptografia), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHanlder.CreateToken(tokenProps);
            return tokenHanlder.WriteToken(token);
        }
        public string EncryptPassword(string password)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(utilRepositorio.GetValueConfigurationHash(configuration));
                aes.IV = new byte[16]; // Vetor de inicialização com 16 bytes (pode ser personalizado ou gerado aleatoriamente)
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream ms = new();
                using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
                {
                    using StreamWriter sw = new(cs);
                    sw.Write(password);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(utilRepositorio.GetValueConfigurationHash(configuration));
            aes.IV = new byte[16]; // O mesmo vetor de inicialização usado na criptografia
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream ms = new(Convert.FromBase64String(encryptedPassword));
            using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new(cs);
            return sr.ReadToEnd();
        }
    }
}
