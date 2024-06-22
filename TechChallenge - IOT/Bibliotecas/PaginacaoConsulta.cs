using Dapper;
using TC_IOC.DBContext;
using static Dapper.SqlMapper;

namespace TC_IOC.Bibliotecas
{
    public class PaginacaoConsulta<T> where T : class
    {
        public long Total { get; set; }
        public IEnumerable<T> Registros { get; set;}
    }

    public enum TipoOrdernacao
    {
        Asc,
        Desc
    }

    public class PaginacaoFiltro(string CampoOrdernacao, TipoOrdernacao tipoOrdernacao)
    {
        public int Pg { get; set; } = 1;
        public int Qt { get; set; } = 10;
        public string CpOrd { get; set; } = CampoOrdernacao;
        public TipoOrdernacao TpOrd { get; set; } = tipoOrdernacao;
    }

    public class RepositorioDapper<T> where T : class
    {
        private readonly DapperContext context;
        public RepositorioDapper(DapperContext dapperContext)
        {
            this.context = dapperContext;
        }
        public PaginacaoConsulta<T> ListarPaginado(string query, DynamicParameters dynamicParameters, int pg, int qt, string cpOrd, string tpOrd) 
        {
            using var con = this.context.CreateConnection();
            PaginacaoConsulta<T> Resultado = new()
            {
                Total = con.Query<int>("SELECT COUNT(1) FROM (" + query + ") T", dynamicParameters).Single(),
                Registros = con.Query<T>(RepositorioDapper<T>.GerarQueryPaginacao(query, pg, qt, cpOrd, tpOrd), dynamicParameters).ToList()
            };
            return Resultado;
        }

        public PaginacaoConsulta<T> ListarPaginado(string query, int pg, int qt, string cpOrd, string tpOrd)
        {
            using var con = this.context.CreateConnection();

            PaginacaoConsulta<T> Resultado = new()
            {
                Total = con.QueryFirst<int>("SELECT COUNT(1) FROM ( " + query + " ) T"),
                Registros = con.Query<T>(RepositorioDapper<T>.GerarQueryPaginacao(query, pg, qt, cpOrd, tpOrd)).ToList()
            };

            return Resultado;
        }

        private static string GerarQueryPaginacao(string query, int pg, int qt, string cpOrd, string tpOrd)
        {
            string text = " ORDER BY " + cpOrd + " " + tpOrd;
            int num = (pg - 1) *qt;
            if(pg == 1)
            {
                return $"{query} {text} LIMIT {qt} ";
            }
            return $"{query} {text} LIMIT {qt} OFFSET {num} ";
        }

        public IEnumerable<T> Listar(string query, DynamicParameters dynamicParameters = null)
        {
            using var con = this.context.CreateConnection();
            if (dynamicParameters == null)
                return con.Query<T>(query);

            return con.Query<T>(query, dynamicParameters);
        }
    }
}
