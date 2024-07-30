using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Regioes.Entidades;

namespace TC_Teste.Contatos.Entidades
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
