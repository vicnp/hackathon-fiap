using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_IOC.Bibliotecas;

namespace TC_Domain.Contatos.Repositorios
{
    public interface IContatosRepositorio
    {
        Contato InserirContato(Contato contato);
        PaginacaoConsulta<Contato> ListarContatos(ContatosFiltro filtro);
    }
}
