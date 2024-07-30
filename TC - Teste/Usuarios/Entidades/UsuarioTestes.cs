using TC_Domain.Usuarios.Entidades;

namespace TC_Teste.Usuarios.Entidades;

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
        int permissao = 1;
        
        //ACT
        var usuario = new Usuario(id, nome, hash, email, permissao);
        var usuarioDefault = new Usuario();

        //ASSERT
        Assert.Equal(0, usuarioDefault.Id);
        Assert.Equal(id, usuario.Id);
        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(hash, usuario.Hash);
        Assert.Equal(email, usuario.Email);
        Assert.Equal(permissao, usuario.Permissao);
    }
}