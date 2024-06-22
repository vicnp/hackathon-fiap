using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_IOC.Bibliotecas;
using TC_IOC.DBContext;

namespace TC_Infra.Contatos
{
    public class ContatosRepositorio(DapperContext dapperContext) : RepositorioDapper<Contato>(dapperContext), IContatosRepositorio
    {
        public PaginacaoConsulta<Contato> ListarContatos(ContatosFiltro filtro)
        {
            string SQL = @"
                        SELECT  c.id,
                                c.nome,
                                c.email,
                                c.ddd,
                                c.telefone
                        FROM TECHCHALLENGE.contatos c
                        LEFT JOIN TECHCHALLENGE.regioes r
                                ON r.ddd = c.ddd

                        WHERE 1 = 1
                        ";

            if(!filtro.Email.IsNullOrEmpty()) 
                SQL += $" AND c.email = '{filtro.Email}' ";

            if(!filtro.Nome.IsNullOrEmpty()) 
                SQL += $" AND c.nome = '{filtro.Nome}' ";

            if(filtro.DDD > 0) 
                SQL += $" AND c.ddd = {filtro.DDD} ";

            if(!filtro.Telefone.IsNullOrEmpty()) 
                SQL += $" AND c.telefone = '{filtro.Telefone}' ";

            if(!filtro.Regiao.IsNullOrEmpty()) 
                SQL += $" AND r.regiao = '{filtro.Regiao}' ";

           return ListarPaginado(SQL, filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString());
        }

    }
}
