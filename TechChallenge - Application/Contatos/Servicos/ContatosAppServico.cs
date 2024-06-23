using AutoMapper;
using TC_Application.Contatos.Interfaces;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Regioes.Repositorios;
using TC_DataTransfer.Regiao.Responses;
using TC_Domain.Regioes.Repositorios.Consultas;

namespace TC_Application.Contatos.Servicos
{
    public class ContatosAppServico(IContatosServico contatosServico,IRegioesRepositorio regioesRepositorio, IMapper mapper) : IContatosAppServico
    {
        public PaginacaoConsulta<ContatoResponse> ListarContatosComPaginacao (ContatoRequest request)
        {
            ContatosFiltro contatosFiltro = mapper.Map<ContatosFiltro>(request);

            PaginacaoConsulta<Contato> consulta = contatosServico.ListarContatos(contatosFiltro);

            PaginacaoConsulta<ContatoResponse> response = mapper.Map<PaginacaoConsulta<ContatoResponse>>(consulta);

            foreach (var contato in response.Registros)
            {
                RegiaoConsulta? regiaoConsulta = regioesRepositorio.ListarRegioes((int)contato.DDD!).FirstOrDefault();

                if(regiaoConsulta == null)
                    continue;

                RegiaoResponse regiaoResponse = mapper.Map<RegiaoResponse>(regiaoConsulta);
                contato.Regiao = regiaoResponse;
            }

            return response;
        }

        public ContatoResponse InserirContato(ContatoInserirRequest request)
        {
            Contato contato = contatosServico.InserirContato(request);
            return  mapper.Map<ContatoResponse>(contato);   
        }

    }
}
