using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Application.Contatos.Interfaces;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using YCTC_DataTransfer.Contatos.Requests;

namespace TC_Application.Contatos.Servicos
{
    public class ContatosAppServico(IContatosServico contatosservico, IMapper mapper) : IContatosAppServico
    {
        public PaginacaoConsulta<ContatoResponse> ListarContatosComPaginacao (ContatoRequest request)
        {
            ContatosFiltro contatosFiltro = mapper.Map<ContatosFiltro>(request);

            PaginacaoConsulta<Contato> paginacaoConsulta = contatosservico.ListarContatos(contatosFiltro);

            return mapper.Map<PaginacaoConsulta<ContatoResponse>>(paginacaoConsulta);
        }

        public ContatoResponse InserirContato(ContatoInserirRequest request)
        {
            Contato contato = contatosservico.InserirContato(request);
            return  mapper.Map<ContatoResponse>(contato);   
        }

    }
}
