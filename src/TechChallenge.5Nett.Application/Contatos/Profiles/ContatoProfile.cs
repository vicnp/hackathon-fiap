using AutoMapper;
using Contatos.Entidades;
using Contatos.Reponses;
using Contatos.Repositorios.Filtros;
using Contatos.Requests;
using Utils;

namespace Contatos.Profiles
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<ContatoPaginacaoRequest, ContatosPaginadosFiltro>();
            CreateMap<ContatoCrudRequest, ContatoFiltro>();
            CreateMap<ContatoRequest, ContatoFiltro>();
            CreateMap<Contato, ContatoResponse>();
            CreateMap<PaginacaoConsulta<Contato>, PaginacaoConsulta<ContatoResponse>>();
        }
    }
}
