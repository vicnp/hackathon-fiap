using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Regioes.Repositorios;
using TC_Domain.Regioes.Repositorios.Consultas;
using TC_IOC.Bibliotecas;
using TC_IOC.DBContext;

namespace TC_Infra.Regioes
{
    public class RegioesRepositorio(DapperContext dapperContext) : RepositorioDapper<RegiaoConsulta>(dapperContext), IRegioesRepositorio
    {
        public List<RegiaoConsulta> ListarRegioes(int ddd = 0)
        {
            string SQL = @"
                            SELECT ddd,
                                   estado,
                                   regiao as Descricao
                            FROM TECHCHALLENGE.regioes
                            WHERE 1 = 1
                         ";
            if(ddd > 0)
            {
                SQL += $@"
                        AND ddd = {ddd}
                        ";
            }
            using var con = dapperContext.CreateConnection();
            return con.Query<RegiaoConsulta>(SQL).ToList();
        }
    }
}
