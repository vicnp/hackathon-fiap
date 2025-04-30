using FluentAssertions;
using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Teste.Usuarios.Entidades;

public class UsuarioTestes
{
    [Fact]
    public void EntidadeUsuarioTestes()
    {
        //ARRANGE
        int id = 1;
        string nome = "Fiap";
        string hash = "qwertyuiopasdfghjklzxcvbnm";
        string email = "fiap@contato.com.br";
        string cpf = "61529748364";

        //ACT
        Usuario usuario = new(id, nome, email, cpf, hash, TipoUsuario.Administrador);
        Usuario usuarioDefault = new();

        //ASSERT
        Assert.Equal(0, usuarioDefault.UsuarioId);
        Assert.Equal(id, usuario.UsuarioId);
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(hash, usuario.Hash);
        Assert.Equal(email, usuario.Email);
        Assert.Equal(cpf, usuario.Cpf);

    }

    [Fact]
    public void EntidadeUsuarioConstructorTestes()
    {
        //ARRANGE
        string nome = "Fiap";
        string hash = "qwertyuiopasdfghjklzxcvbnm";
        string email = "fiap@contato.com.br";
        string cpf = "61529748364";

        //ACT
        Usuario usuario = new(nome, email, cpf, hash, TipoUsuario.Administrador);
        Usuario usuarioDefault = new();

        //ASSERT
        Assert.Equal(0, usuarioDefault.UsuarioId);
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(hash, usuario.Hash);
        Assert.Equal(email, usuario.Email);
        Assert.Equal(cpf, usuario.Cpf);

    }

    [Fact]
    public void EntidadeUsuarioComandoConstructorTestes()
    {
        //ARRANGE
        string nome = "Fiap";
        string hash = "qwertyuiopasdfghjklzxcvbnm";
        string email = "fiap@contato.com.br";
        string cpf = "61529748364";

        //ACT
        UsuarioCadastroComando usuario = new() { Nome = nome, Email = email, Cpf = cpf, Crm = hash, Senha = "senha", SobreNome = "Teste", TipoUsuario = TipoUsuario.Medico };

        //ASSERT
        usuario.Crm.Should().Be(hash);
        usuario.Email.Should().Be(email);
        usuario.Cpf.Should().Be(cpf);
        usuario.Senha.Should().Be("senha");
        usuario.SobreNome.Should().Be("Teste");
        usuario.TipoUsuario.Should().Be(TipoUsuario.Medico);
    }
}