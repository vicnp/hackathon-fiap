using TC_Domain.Contatos.Entidades;
using TC_Domain.Regioes.Entidades;

namespace TC_Teste.Contatos.Entidades;

public class ContatoTestes
{
    [Fact]
    public void EntidadeContatoTestes()
    {
        //ARRANGE
        var nome = "Fiap Contato";
        var email = "fiap@contato.com.br";
        var ddd = 11;
        var telefone = "912345678";
        
        //ACT
        var contato = new Contato(nome, email, ddd, telefone);
        
        
        //ASSERT
        Assert.Equal(nome, contato.Nome);
        Assert.Equal(email, contato.Email);
        Assert.Equal(ddd, contato.DDD);
        Assert.Equal(telefone, contato.Telefone);
    }
}

public class ContatoSetersTestes
{
    [Fact]
    public void Quando_DefinirId_Espero_Id()
    {
        //ARRANGE
        var id = 1;
        var nome = "Fiap Contato";
        var email = "fiap@contato.com.br";
        var ddd = 11;
        var telefone = "912345678";
        
        //ACT
        var contato = new Contato(nome, email, ddd, telefone);
        
        contato.SetId(id);
        
        //ASSERT
        Assert.Equal(id, contato.Id);
    }
    
    [Fact]
    public void Quando_DefinirRegiao_Espero_Regiao()
    {
        //ARRANGE
        var nome = "Fiap Contato";
        var email = "fiap@contato.com.br";
        var ddd = 11;
        var telefone = "912345678";

        var estado = "SP";
        var descricao = "Sudeste";
        
        //ACT
        var contato = new Contato(nome, email, ddd, telefone);
        var regiao = new Regiao(ddd, estado, descricao);
        
        contato.SetRegiao(regiao);
        
        //ASSERT
        Assert.Equal(regiao, contato.Regiao);
    }
}