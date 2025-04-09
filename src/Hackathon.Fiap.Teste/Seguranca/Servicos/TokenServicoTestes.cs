using FluentAssertions;
using Hackathon.Fiap.Domain.Seguranca.Servicos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Hackathon.Fiap.Domain.Utils.Repositorios;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Hackathon.Fiap.Teste.Seguranca.Servicos
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
                var ct = CancellationToken.None;

                
                utilRepositorio.GetValueConfigurationHash(configuration).Returns("6i9BiR4fRpbbIKxxEoEyjQ==");
                utilRepositorio.GetValueConfigurationKeyJWT(configuration).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
                usuariosRepositorio.RecuperarUsuarioAsync(email, Arg.Any<string>(), Arg.Any<CancellationToken>())
                    .ReturnsNull();
                
                await FluentActions.Awaiting(() => tokenServico.GetTokenAsync(email, senha, ct))
                    .Should().ThrowAsync<NaoAutorizadoExcecao>()
                    .WithMessage("Usuário ou senha incorretos.");
                
                // await FluentActions.Awaiting(() => tokenServico.GetTokenAsync(email, senha, ct))
                //     .Should().NotThrowAsync();

                //Caso usuario encontrado!!
                //ARRANGE
                int id = 1;
                string nome = "Fiap";
                string cpf = "61529748364";

                //ACT
                var usuario = new Usuario(id, nome, email, cpf, hash, TipoUsuario.Administrador);
                usuariosRepositorio.RecuperarUsuarioAsync(email, Arg.Any<string>(), ct).Returns(usuario);
                await FluentActions.Awaiting(() => tokenServico.GetTokenAsync(email, senha, ct))
                    .Should().NotThrowAsync();
            }
        }

        public class DecryptPasswordMetodo : TokenServicoTestes
        {
            [Fact]
            public void Quando_DecryptPassword_Espero_ObjValidos()
            {
                string encryptedPassword = "Ot23Q5J0ZfcMXMFRGdbF0OxTFavCzXQ6PROYafL2HiU=";
                utilRepositorio.GetValueConfigurationKeyJWT(configuration).Returns("6i9BiR4fRpbbIKxxEoEyjQ==");
                tokenServico.Invoking(x => x.DecryptPassword(encryptedPassword)).Should().NotThrow();
            }

        }
    }
}
