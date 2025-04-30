using Dapper;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hackathon.Fiap.Infra.Medicos
{
    public class EspecialidadesRepositorio(DapperContext dapperContext) : RepositorioDapper<Especialidade>(dapperContext), IEspecialidadesRepositorio
    {

        public Task DeletarEspecialidadeAsync(int especialidadeId, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                       DELETE FROM Especialidade
                                       WHERE id = @ID;
                                     ");

            DynamicParameters dp = new();
            dp.Add("@ID", especialidadeId);
            return session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<Especialidade> InserirEspecialidadeAsync(Especialidade especialidade, CancellationToken ct)
        {
            const string sql = @"
                INSERT INTO Especialidade (nome, descricao)
                    VALUES (@Nome, @Descricao);
                SELECT LAST_INSERT_ID();";

            var parametros = new
            {
                Nome = especialidade.NomeEspecialidade,
                Descricao = especialidade.DescricaoEspecialidade
            };

            var id = await session.QueryFirstOrDefaultAsync<int>(
                new CommandDefinition(sql, parametros, commandTimeout: 30, transaction: null, cancellationToken: ct)
            );

            especialidade.EspecialidadeId = id;
            return especialidade;
        }

        public Task<PaginacaoConsulta<Especialidade>> ListarEspecialidadesMedicosPaginadosAsync(EspecialidadePaginacaoFiltro filtro, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                    SELECT e.id as EspecialidadeId,
                                           e.nome as NomeEspecialidade,
                                           e.descricao as DescricaoEspecialidade
                                     FROM Especialidade e
	                                     WHERE 1 = 1");

            if (!filtro.NomeEspecialidade.IsNullOrEmpty())
                sql.AppendLine($@" AND e.nome = '{filtro.NomeEspecialidade}' ");


            if (filtro.CodigoEspecialidade.HasValue)
                sql.AppendLine($@" AND e.id = '{filtro.CodigoEspecialidade}' ");

            return ListarPaginadoAsync(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString(), ct: ct);
        }

        public Task<Especialidade?> RecuperarEspecialidadeAsync(int especialidadeId, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                     SELECT e.id as EspecialidadeId,
                                            e.nome as NomeEspecialidade,
                                            e.descricao as DescricaoEspecialidade
                                     FROM Especialidade e
                                      WHERE u.hash = @identificador");

            DynamicParameters dp = new();
            dp.Add("identificador", especialidadeId);

            return session.QueryFirstOrDefaultAsync<Especialidade>(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }
    }
}
