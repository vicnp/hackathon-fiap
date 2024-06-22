using AutoMapper;
using LHS_IOT.Bibliotecas;
using LHS_DataTransfer.RequisicoesConteudo.Response;
using LHS_Domain.RequisicoesConteudo.Entidades;
using LHS_Domain.RequisicoesConteudo.Enumerators;

namespace LHS_Application.RequisicoesConteudo.Profiles
{
    public class RequisicoesConteudoProfile: Profile
    {
        public RequisicoesConteudoProfile()
        {
            CreateMap<RequisicaoConteudo, RequisicaoConteudoResponse>();
            CreateMap<SituacaoRequisicaoEnum, LHS_IOT.Bibliotecas.EnumValue>().ConvertUsing(scr => scr.GetValue());
            CreateMap<PaginacaoConsulta<RequisicaoConteudo>, PaginacaoConsulta<RequisicaoConteudoResponse>>();
        }
    }
}
