using Dapper;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;
using System.Data;

namespace Hackathon.Fiap.Infra.Utils
{
    public class RepositorioDapper<T>(DapperContext dapperContext) where T : class
    {
        public readonly IDbConnection session = dapperContext.CreateConnection();

        public PaginacaoConsulta<T> ListarPaginado(string query, DynamicParameters dynamicParameters, int pg, int qt, string cpOrd, string tpOrd)
        {

            PaginacaoConsulta<T> Resultado = new()
            {
                Total = session.Query<int>("SELECT COUNT(1) FROM (" + query + ") T", dynamicParameters).Single(),
                Registros = session.Query<T>(RepositorioDapper<T>.GerarQueryPaginacao(query, pg, qt, cpOrd, tpOrd), dynamicParameters).ToList()
            };
            return Resultado;
        }

        public PaginacaoConsulta<T> ListarPaginado(string query, int pg, int qt, string cpOrd, string tpOrd)
        {
            PaginacaoConsulta<T> Resultado = new()
            {
                Total = RecuperarTotalLinhas(query),
                Registros = session.Query<T>(RepositorioDapper<T>.GerarQueryPaginacao(query, pg, qt, cpOrd, tpOrd)).ToList()
            };

            return Resultado;
        }

        public int RecuperarTotalLinhas(string query)
        {
            return session.QueryFirst<int>("SELECT COUNT(1) FROM ( " + query + " ) T");
        }

        public static string GerarQueryPaginacao(string query, int pg, int qt, string cpOrd, string tpOrd)
        {
            string text = " ORDER BY " + cpOrd + " " + tpOrd;
            int num = (pg - 1) * qt;
            if (pg == 1)
            {
                return $"{query} {text} LIMIT {qt} ";
            }
            return $"{query} {text} LIMIT {qt} OFFSET {num} ";
        }

        public IEnumerable<T> Listar(string query, DynamicParameters dynamicParameters = null)
        {
            if (dynamicParameters == null)
                return session.Query<T>(query);

            return session.Query<T>(query, dynamicParameters);
        }
    }
}
