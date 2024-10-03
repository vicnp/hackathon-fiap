using AutoMapper;
using Regioes.Entidades;
using Regioes.Responses;

namespace Regioes.Profiles
{
    public class RegioesProfile : Profile
    {
        public RegioesProfile()
        {
            CreateMap<Regiao, RegiaoResponse>();
        }
    }
}
