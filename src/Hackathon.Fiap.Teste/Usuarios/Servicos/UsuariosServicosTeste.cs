using FluentAssertions;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Usuarios.Servicos;
using Hackathon.Fiap.Domain.Usuarios.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;

namespace Hackathon.Fiap.Teste.Usuarios.Servicos
{
    public class UsuariosServicosTeste
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly ITokenServico tokenServico;
        private readonly IUsuariosServico sut;
        public UsuariosServicosTeste()
        {
            usuariosRepositorio = Substitute.For<IUsuariosRepositorio>();
            tokenServico = Substitute.For<ITokenServico>();

            sut = new UsuariosServico(usuariosRepositorio, tokenServico);
        }

        public class CadastrarUsuarioAsyncTeste : UsuariosServicosTeste
        {
            [Fact]
            public async Task Dado_Usuario_Valido_Espero_Usuario_Cadastrado()
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = "437.959.990-65",
                    Crm = "04214/SP",
                    Email = "teste@tester.com",
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = "1234",
                    TipoUsuario = TipoUsuario.Administrador
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                string email = "fiap@contato.com.br";
                string cpf = "437.959.990-65";

                //ACT
                Usuario usuarioCadastro = new(nome, email, cpf, hash, TipoUsuario.Administrador);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Usuario>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                Usuario usuarioResponse = await sut.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken);
                usuarioResponse.Should().NotBeNull();
                usuarioCadastro.Tipo.Should().Be(TipoUsuario.Administrador);
                usuarioCadastro.Nome.Should().Be(nome);
                usuarioCadastro.Hash.Should().Be(hash);
                usuarioCadastro.Cpf.Should().Be(cpf);
            }

            [Fact]
            public async Task Dado_Medico_Valido_Espero_Usuario_Cadastrado()
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = "437.959.990-65",
                    Crm = "04214/SP",
                    Email = "teste@tester.com",
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = "1234",
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                string email = "fiap@contato.com.br";
                string cpf = "437.959.990-65";

                //ACT
                Usuario usuarioCadastro = new(nome, email, cpf, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                Usuario usuarioResponse = await sut.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken);
                usuarioResponse.Should().NotBeNull();
                usuarioCadastro.Tipo.Should().Be(TipoUsuario.Medico);
                usuarioCadastro.Nome.Should().Be(nome);
                usuarioCadastro.Hash.Should().Be(hash);
                usuarioCadastro.Cpf.Should().Be(cpf);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("abc")]
            public async Task Dado_Medico_CRM_Invalido_Espero_Excecao(string CRM)
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = "165.904.007-84",
                    Crm = CRM,
                    Email = "teste@tester.com",
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = "1234",
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                string email = "fiap@contato.com.br";
                string cpf = "61529748364";

                //ACT
                Usuario usuarioCadastro = new(nome, email, cpf, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                await sut.Invoking(x => x.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("abc")]
            public async Task Dado_Medico_Email_Invalido_Espero_Excecao(string emailTeste)
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = "165.904.007-84",
                    Crm = "012215/ES",
                    Email = emailTeste,
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = "1234",
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                string cpf = "61529748364";

                //ACT
                Usuario usuarioCadastro = new(nome, emailTeste, cpf, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                await sut.Invoking(x => x.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("a")]
            public async Task Dado_Medico_Senha_Invalido_Espero_Excecao(string senha)
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = "165.904.007-84",
                    Crm = "012215/ES",
                    Email = "emailTeste@teste.com",
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = senha,
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                string cpf = "61529748364";

                //ACT
                Usuario usuarioCadastro = new(nome, usuarioCadastroComando.Email, cpf, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                await sut.Invoking(x => x.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData(null, "asd")]
            [InlineData("", "sim")]
            [InlineData("a", "a")]
            [InlineData("aaa", "")]
            [InlineData("asd", null)]
            public async Task Dado_Medico_Nome_SobreNome_Invalido_Espero_Excecao(string nomeTeste, string sobreNome)
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = "165.904.007-84",
                    Crm = "012215/ES",
                    Email = "emailTeste@teste.com",
                    Nome = nomeTeste,
                    SobreNome = sobreNome,
                    Senha = "124124",
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";
                string cpf = "61529748364";

                //ACT
                Usuario usuarioCadastro = new(nome, usuarioCadastroComando.Email, cpf, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                await sut.Invoking(x => x.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("abcasd")]
            [InlineData("165.901.005-84")]
            [InlineData("125.904.005-84")]
            [InlineData("165.904.005-01")]
            [InlineData("132.904.005-01")]
            [InlineData("732.904.005-01")]
            [InlineData("732.904.005-05")]
            [InlineData("002.904.005-05")]
            [InlineData("002.204.555-05")]
            [InlineData("002.204.555-00")]
            [InlineData("111.111.111-11")]
            public async Task Dado_Medico_Cpf_Invalido_Espero_Excecao(string cpfTeste)
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = cpfTeste,
                    Crm = "012215/ES",
                    Email = "teste@tester.com",
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = "12324",
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";

                //ACT
                Usuario usuarioCadastro = new(nome, usuarioCadastroComando.Email, cpfTeste, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                await sut.Invoking(x => x.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken)).Should().ThrowAsync<RegraDeNegocioExcecao>();
            }

            [Theory]
            [InlineData("437.959.990-65")]
            [InlineData("238.619.770-01")]
            [InlineData("247.241.370-02")]
            [InlineData("116.410.470-54")]
            [InlineData("579.216.950-00")]
            [InlineData("044.696.840-40")]
            [InlineData("680.618.340-90")]
            [InlineData("649.739.030-86")]
            [InlineData("006.983.010-05")]
            [InlineData("324.992.350-80")]
            [InlineData("946.203.850-34")]
            [InlineData("215.869.710-50")]
            [InlineData("301.635.420-90")]
            [InlineData("123.456.789-09")]
            public async Task Dado_Medico_Cpf_Valido_Espero_Excecao(string cpfTeste)
            {
                UsuarioCadastroComando usuarioCadastroComando = new()
                {
                    Cpf = cpfTeste,
                    Crm = "012215/ES",
                    Email = "teste@tester.com",
                    Nome = "Quality",
                    SobreNome = "Assurance",
                    Senha = "12324",
                    TipoUsuario = TipoUsuario.Medico
                };

                string nome = "Fiap";
                string hash = "qwertyuiopasdfghjklzxcvbnm";

                //ACT
                Usuario usuarioCadastro = new(nome, usuarioCadastroComando.Email, cpfTeste, hash, TipoUsuario.Medico);

                CancellationToken cancellationToken = new();

                usuariosRepositorio.InserirUsuarioAsync(Arg.Any<Medico>(), cancellationToken).Returns(usuarioCadastro);
                tokenServico.EncryptPassword(Arg.Any<string>()).Returns("5-97FSAY*FASH#@$=asoihfdasf");

                await sut.Invoking(x => x.CadastrarUsuarioAsync(usuarioCadastroComando, cancellationToken)).Should().NotThrowAsync<RegraDeNegocioExcecao>();
            }
        }
    }
}
