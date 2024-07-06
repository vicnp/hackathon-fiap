using AutoMapper;
using TC_Application.Contatos.Interfaces;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using TC_Domain.Regioes.Repositorios;
using TC_DataTransfer.Regiao.Responses;
using TC_Domain.Utils;
using TC_Domain.Regioes.Entidades;
using MySqlX.XDevAPI;
using TC_Infra.Contatos;

namespace TC_Application.Contatos.Servicos
{
    public class ContatosAppServico(IContatosServico contatosServico,IRegioesRepositorio regioesRepositorio, IMapper mapper) : IContatosAppServico
    {
        public PaginacaoConsulta<ContatoResponse> ListarContatosComPaginacao (ContatoRequest request)
        {
            ContatosPaginadosFiltro contatosFiltro = mapper.Map<ContatosPaginadosFiltro>(request);

            PaginacaoConsulta<Contato> consulta = contatosServico.ListarContatos(contatosFiltro);

            PaginacaoConsulta<ContatoResponse> response = mapper.Map<PaginacaoConsulta<ContatoResponse>>(consulta);

            return response;
        }

        public ContatoResponse InserirContato(ContatoCrudRequest request)
        {
            ContatoFiltro contatoFiltro = mapper.Map<ContatoFiltro>(request);

            Contato contato = contatosServico.InserirContato(contatoFiltro);

            ContatoResponse contatoResponse = mapper.Map<ContatoResponse>(contato);

            Regiao? regiaoConsulta = regioesRepositorio.ListarRegioes((int)contatoResponse.DDD!).FirstOrDefault();
            RegiaoResponse regiaoResponse = mapper.Map<RegiaoResponse>(regiaoConsulta);

            contatoResponse.Regiao = regiaoResponse;
            return contatoResponse;
        }

        public ContatoResponse? AtualizarContato(ContatoCrudRequest request, int id)
        {
            Contato contatoAtualizado = contatosServico.RecuperarContato(id);
            if (contatoAtualizado == null) {
                return null;
            }
            contatoAtualizado.SetDDD((int)request.DDD!);
            contatoAtualizado.SetEmail(request.Email!);
            contatoAtualizado.SetNome(request.Nome!);    
            contatoAtualizado.SetTelefone(request.Telefone!);    
            return mapper.Map<ContatoResponse>(contatosServico.AtualizarContato(contatoAtualizado));
        }

        public void RemoverContato(int id)
        {
            contatosServico.RemoverContato(id);
        }

    }
}
