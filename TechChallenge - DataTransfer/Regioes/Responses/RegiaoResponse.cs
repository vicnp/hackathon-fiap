using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_DataTransfer.Regiao.Responses
{
    public class RegiaoResponse
    {
        public int RegiaoDDD {  get; set; }
        public string? Estado {  get; set; }
        public string? Descricao{ get; set; }

        public RegiaoResponse()
        {
            
        }

        public RegiaoResponse(int dDD, string estado, string descricao)
        {
            RegiaoDDD = dDD;
            Estado = estado;
            Descricao = descricao;
        }
    }
}
