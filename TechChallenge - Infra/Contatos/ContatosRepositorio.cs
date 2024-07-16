
using Dapper;
using Microsoft.IdentityModel.Tokens;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Regioes.Entidades;
using TC_Domain.Utils;
using TC_IOC.Bibliotecas;
using TC_IOC.DBContext;

namespace TC_Infra.Contatos
{
    public class ContatosRepositorio(DapperContext dapperContext) : RepositorioDapper<Contato>(dapperContext), IContatosRepositorio
    {
        public PaginacaoConsulta<Contato> ListarPaginacaoContatos(ContatosPaginadosFiltro filtro)
        {
            string SQL = @"
                        SELECT  c.id,
                                c.nome,
                                c.email,
                                c.ddd,
                                c.telefone,
                                r.ddd as RegiaoDDD,
                                r.estado,
                                r.regiao as Descricao 
                        FROM TECHCHALLENGE.contatos c
                        LEFT JOIN TECHCHALLENGE.regioes r
                                ON r.ddd = c.ddd

                        WHERE 1 = 1
                        ";

            if(!filtro.Email.IsNullOrEmpty()) 
                SQL += $" AND c.email = '{filtro.Email}' ";

            if(!filtro.Nome.IsNullOrEmpty()) 
                SQL += $" AND c.nome like '%{filtro.Nome}%' ";

            if(filtro.DDD > 0) 
                SQL += $" AND c.ddd = {filtro.DDD} ";

            if(!filtro.Telefone.IsNullOrEmpty()) 
                SQL += $" AND c.telefone = '{filtro.Telefone}' ";

            if(!filtro.Regiao.IsNullOrEmpty()) 
                SQL += $" AND r.regiao like '%{filtro.Regiao}%' ";

           string sqlPaginado = GerarQueryPaginacao(SQL, filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString());

            //IEnumerable<Contato> registros = session.Query<Contato>(sqlPaginado);

            var registros = new Dictionary<int, Contato>();

            var queryResult = session.Query<Contato, Regiao, Contato>(sqlPaginado, (contato, regiao) =>
            {
                if (!registros.TryGetValue(contato.Id.Value, out var existingContato))
                {
                    existingContato = contato;
                    registros[contato.Id.Value] = existingContato;
                }
                existingContato.SetRegiao(regiao);
                return existingContato;
            }, splitOn: "RegiaoDDD");

            PaginacaoConsulta<Contato> response = new()
            {
                Registros = registros.Values,
                Total = RecuperarTotalLinhas(SQL)
            };

           return response;
        }

        public List<Contato> ListarContatos(ContatoFiltro filtro)
        {
            string SQL = @"
                        SELECT  c.id,
                                c.nome,
                                c.email,
                                c.ddd,
                                c.telefone,
                                r.ddd as RegiaoDDD,
                                r.estado,
                                r.regiao as Descricao 
                        FROM TECHCHALLENGE.contatos c
                        LEFT JOIN TECHCHALLENGE.regioes r
                                ON r.ddd = c.ddd

                        WHERE 1 = 1
                        ";

            if (!filtro.Email.IsNullOrEmpty())
                SQL += $" AND c.email = '{filtro.Email}' ";

            if (!filtro.Nome.IsNullOrEmpty())
                SQL += $" AND c.nome like '%{filtro.Nome}%' ";

            if (filtro.DDD > 0)
                SQL += $" AND c.ddd = {filtro.DDD} ";

            if (!filtro.Telefone.IsNullOrEmpty())
                SQL += $" AND c.telefone = '{filtro.Telefone}' ";

            if (!filtro.Regiao.IsNullOrEmpty())
                SQL += $" AND r.regiao like '%{filtro.Regiao}%' ";


            return session.Query<Contato, Regiao, Contato>(SQL, (contato, regiao) =>
            {
                contato.SetRegiao(regiao);
                return contato;
            }, splitOn: "RegiaoDDD").ToList();

        }


        public Contato InserirContato(Contato contato)
        {
            string SQL = @"
                            INSERT INTO TECHCHALLENGE.contatos
                                   (nome, email, ddd, telefone)
                            VALUES(@NOME, @EMAIL, @DDD, @TELEFONE);
                            SELECT c.id,
                                   r.ddd,
                                   r.estado,
                                   r.regiao AS Descricao
                              FROM TECHCHALLENGE.contatos c
                              JOIN TECHCHALLENGE.regioes r ON c.ddd = r.ddd
                             WHERE c.id = LAST_INSERT_ID();
                        ";

            DynamicParameters parametros = new();
            parametros.Add("@NOME", contato.Nome);
            parametros.Add("@EMAIL", contato.Email);
            parametros.Add("@DDD", contato.DDD);
            parametros.Add("@TELEFONE", contato.Telefone);

            var result = session.QuerySingle<dynamic>(SQL, parametros);
            Regiao regiao = new(result.ddd, result.estado, result.Descricao);
            contato.SetId(result.id);
            contato.SetRegiao(regiao); 
            return contato;
        }

        public Contato RecuperarContato(int id) {
            string SQL = $@"
                        SELECT  c.id,
                                c.nome,
                                c.email,
                                c.ddd,
                                c.telefone,
                                r.ddd as RegiaoDDD,
                                r.estado,
                                r.regiao as Descricao 
                        FROM TECHCHALLENGE.contatos c
                        LEFT JOIN TECHCHALLENGE.regioes r
                                ON r.ddd = c.ddd

                        WHERE c.id = {id}
                        ";
            return session.QueryFirst<Contato>(SQL);
        }

        public void RemoverContato(int id ) {
            string SQL = $@"
                        DELETE FROM TECHCHALLENGE.contatos WHERE id= {id};
                        ";

            session.Execute(SQL);
        }

        public Contato AtualizarContato(Contato contato) {
            string SQL = $@"
                        UPDATE TECHCHALLENGE.contatos
                        SET nome='{contato.Nome}', email='{contato.Email}', ddd={contato.DDD}, telefone='{contato.Telefone}'
                        WHERE id= {contato.Id};
                ";
            session.Execute(SQL);
            return RecuperarContato((int)contato.Id!);
        }
    }
}
