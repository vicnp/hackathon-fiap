using AutoMapper;
using TC_DataTransfer.Regiao.Responses;
using TC_Domain.Regioes.Entidades;

namespace TC_Application.Regioes.Profiles
{
    public class RegioesProfile: Profile
    {
        public RegioesProfile()
        {
            CreateMap<Regiao, RegiaoResponse>();    
        }
    }
}
