﻿using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Utils
{
    public class PaginacaoFiltro(string CampoOrdernacao, TipoOrdernacao tipoOrdernacao)
    {
        public int Pg { get; set; } = 1;
        public int Qt { get; set; } = 10;
        public string CpOrd { get; set; } = CampoOrdernacao;
        public TipoOrdernacao TpOrd { get; set; } = tipoOrdernacao;
    }
}
