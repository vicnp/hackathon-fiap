
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
        public async Task<PaginacaoConsulta<Contato>> ListarPaginacaoContatosAsync(ContatosPaginadosFiltro filtro)
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

            
            var registros = new Dictionary<int, Contato>();

            var queryResult = await session.QueryAsync<Contato, Regiao, Contato>(sqlPaginado, (contato, regiao) =>
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

        public async Task<List<Contato>> ListarContatosAsync(ContatoFiltro filtro)
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

            var result =  await session.QueryAsync<Contato, Regiao, Contato>(SQL, (contato, regiao) =>
            {
                contato.SetRegiao(regiao);
                return contato;
            }, splitOn: "RegiaoDDD");

            return result.ToList();
        }


        public async Task<Contato> InserirContatoAsync(Contato contato)
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

            var result = await session.QuerySingleAsync<dynamic>(SQL, parametros);
            Regiao regiao = new(result.ddd, result.estado, result.Descricao);
            contato.SetId(result.id);
            contato.SetRegiao(regiao); 
            return contato;
        }

        public async Task<Contato> RecuperarContatoAsync(int id) {
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
            return await session.QueryFirstOrDefaultAsync<Contato>(SQL);
        }

        public async Task RemoverContatoAsync(int id) {
            string SQL = $@"
                        DELETE FROM TECHCHALLENGE.contatos WHERE id= {id};
                        ";

            await session.ExecuteAsync(SQL);
        }

        public async Task<Contato> AtualizarContatoAsync(Contato contato) {
            string SQL = $@"
                        UPDATE TECHCHALLENGE.contatos
                        SET nome='{contato.Nome}', email='{contato.Email}', ddd={contato.DDD}, telefone='{contato.Telefone}'
                        WHERE id= {contato.Id};
                ";
            await session.ExecuteAsync(SQL);
            return await RecuperarContatoAsync((int)contato.Id!);
        }
    }
}
