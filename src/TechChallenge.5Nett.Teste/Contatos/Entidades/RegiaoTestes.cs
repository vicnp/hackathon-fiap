using Regioes.Entidades;

namespace Contatos.Entidades
{
    public class RegiaoTestes
    {
        [Fact]
        public void Quando_EstanciaRegiao_Espero_Objeto_Integro()
        {
            var regiaoDefault = new Regiao();
            //ASSERT
            Assert.Equal(0, regiaoDefault.RegiaoDDD);
        }
    }
}
