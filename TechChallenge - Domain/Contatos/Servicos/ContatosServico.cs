using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;

namespace TC_Domain.Contatos.Servicos
{
    public class ContatosServico(IContatosRepositorio contatosRepositorio) : IContatosServico
    {
        public PaginacaoConsulta<Contato> ListarContatos (ContatosFiltro request)
        {
            PaginacaoConsulta<Contato> consultaPaginada = contatosRepositorio.ListarContatos(request);
            return consultaPaginada;
        }
    }
}
