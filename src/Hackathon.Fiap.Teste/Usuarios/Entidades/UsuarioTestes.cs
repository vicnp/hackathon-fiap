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
        var usuario = new Usuario(id, nome, email, cpf, hash, TipoUsuario.Administrador);
        var usuarioDefault = new Usuario();

        //ASSERT
        Assert.Equal(0, usuarioDefault.UsuarioId);
        Assert.Equal(id, usuario.UsuarioId);
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(hash, usuario.Hash);
        Assert.Equal(email, usuario.Email);
        Assert.Equal(cpf, usuario.Cpf);

    }
}