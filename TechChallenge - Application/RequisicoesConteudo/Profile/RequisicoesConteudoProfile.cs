using AutoMapper;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.RequisicoesConteudo.Response;
using TC_Domain.RequisicoesConteudo.Entidades;
using TC_Domain.RequisicoesConteudo.Enumerators;

namespace TC_Application.RequisicoesConteudo.Profiles
{
    public class RequisicoesConteudoProfile: Profile
    {
        public RequisicoesConteudoProfile()
        {
            CreateMap<RequisicaoConteudo, RequisicaoConteudoResponse>();
            CreateMap<SituacaoRequisicaoEnum, TC_IOC.Bibliotecas.EnumValue>().ConvertUsing(scr => scr.GetValue());
            CreateMap<PaginacaoConsulta<RequisicaoConteudo>, PaginacaoConsulta<RequisicaoConteudoResponse>>();
        }
    }
}
