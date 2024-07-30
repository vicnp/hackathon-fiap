using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Text;
using TC_Domain.Seguranca.Servicos;
using TC_Domain.Usuarios.Entidades;
using TC_Domain.Usuarios.Repositorios;
using TC_Domain.Utils.Repositorios;

namespace TC_Teste.Seguranca.Servicos
{
    public class TokenServicoTestes
    {
        private readonly IConfiguration configuration;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IUtilRepositorio utilRepositorio;
        private readonly TokenServico tokenServico;

        public TokenServicoTestes()
        {
            usuariosRepositorio = Substitute.For<IUsuariosRepositorio>();
            utilRepositorio = Substitute.For<IUtilRepositorio>();
            configuration = Substitute.For<IConfiguration>();

            tokenServico = new TokenServico(configuration, usuariosRepositorio, utilRepositorio);
        }

        public class GetTokenMetodo : TokenServicoTestes
        {
            [Fact]
            public async Task Quando_GetToken_Espero_ObjValidos()
            {
                //Caso usuario não encontrado!!
                string email = "fiap@contato.com.br";
                string senha = "pastel de frango";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                utilRepositorio.GetValueConfigurationHash(configuration).Returns("6i9BiR4fRpbbIKxxEoEyjQ==");
                utilRepositorio.GetValueConfigurationKeyJWT(configuration).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
                usuariosRepositorio.RecuperarUsuario(email, Arg.Any<string>()).Returns(x => null);
                tokenServico.Invoking(x => x.GetToken(email, senha)).Should().NotThrow();

                //Caso usuario encontrado!!
                //ARRANGE
                int id = 1;
                string nome = "Fiap";
                int permissao = 1;

                //ACT
                var usuario = new Usuario(id, nome, hash, email, permissao);
                usuariosRepositorio.RecuperarUsuario(email, Arg.Any<string>()).Returns(usuario);
                tokenServico.Invoking(x => x.GetToken(email, senha)).Should().NotThrow();
            }
        }

        public class DecryptPasswordMetodo : TokenServicoTestes
        {
            [Fact]
            public async Task Quando_DecryptPassword_Espero_ObjValidos()
            {
                string encryptedPassword = "Ot23Q5J0ZfcMXMFRGdbF0OxTFavCzXQ6PROYafL2HiU=";
                utilRepositorio.GetValueConfigurationHash(configuration).Returns("6i9BiR4fRpbbIKxxEoEyjQ==");
                tokenServico.Invoking(x => x.DecryptPassword(encryptedPassword)).Should().NotThrow();
            }
        }
    }
}
