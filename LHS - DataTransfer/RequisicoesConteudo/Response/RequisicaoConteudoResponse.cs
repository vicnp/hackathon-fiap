
using LHS_Domain.RequisicoesConteudo.Enumerators;
using LHS_IOT.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHS_DataTransfer.RequisicoesConteudo.Response
{
    public class RequisicaoConteudoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }  
        public string Ano {  get; set; }
        public string Observacao { get; set;}
        public EnumValue Situacao { get; set;}
        public string Referencia { get; set;}
    }
}
