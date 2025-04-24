using Dapper;
using System.Text;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Hackathon.Fiap.Domain.Utils.Helpers;

namespace Hackathon.Fiap.Infra.Medicos
{
    public class MedicosRepositorio(DapperContext dapperContext) : RepositorioDapper<Medico>(dapperContext), IMedicosRepositorio
    {
        public async Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
                @"SELECT
	                    u.id as UsuarioId,
	                    u.cpf as Cpf,
	                    u.email as Email,
	                    u.nome as Nome,
	                    u.tipo as TipoUsuario,
                        m.crm as Crm,
	                    u.criado_em as CriadoEm,
	                    e.id as EspecialidadeId,
	                    e.nome as NomeEspecialidade,
	                    e.descricao as DescricaoEspecialidade
                    FROM
	                    techchallenge.Usuario u
                    INNER JOIN techchallenge.Medico m 
                    ON m.id = u.id 
                    INNER JOIN techchallenge.Medico_Especialidade me 
                    ON me.medico_id = u.id 
                    INNER JOIN techchallenge.Especialidade e 
                    ON e.id = me.especialidade_id 
                  WHERE u.tipo = 'Medico'");

            if (filtro.Id > 0)
            {
                sql.AppendLine($" AND u.id = @MEDICOID ");
                dp.Add("@MEDICOID", filtro.Id);
            }

            if (!filtro.Email.InvalidOrEmpty())
            {
                sql.AppendLine($" AND u.email = @EMAIL ");
                dp.Add("@EMAIL", filtro.Email);
            }

            if (!filtro.Nome.InvalidOrEmpty())
            {
                sql.AppendLine($" AND u.nome like '%@NOME%' ");
                dp.Add("@NOME", filtro.Nome);
            }

            if (!filtro.Crm.InvalidOrEmpty())
            {
                sql.AppendLine($" AND m.crm = @CRM ");
                dp.Add("@CRM", filtro.Crm);
            }

            if (!filtro.CodigoEspecialidade.HasValue && filtro.CodigoEspecialidade != null)
            {
                sql.AppendLine($" AND e.id = @ESPECIALIDADEID ");
                dp.Add("@ESPECIALIDADEID", filtro.CodigoEspecialidade.Value);
            }

            if (!filtro.NomeEspecialidade.InvalidOrEmpty())
            {
                sql.AppendLine($" AND e.nome like '%@NOMESPECIALIDADE%' ");
                dp.Add("@NOMESPECIALIDADE", filtro.NomeEspecialidade);
            }

            string sqlPaginado = GerarQueryPaginacao(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString());

            var registros = new Dictionary<int, Medico>();

            Task<IEnumerable<Medico>> task = session.QueryAsync<Medico, Especialidade, Medico>(sqlPaginado, (medico, especialidade) =>
            {
                if (!registros.TryGetValue(medico.UsuarioId, out var existingMedico))
                {
                    existingMedico = medico;
                    registros[medico.UsuarioId] = existingMedico;
                }
                existingMedico.SetEspecialidade(especialidade);
                return existingMedico;
            }, splitOn: "EspecialidadeId", param: dp);

            await Task.Run(async () => {
                await task;
            }, ct);

            PaginacaoConsulta<Medico> response = new()
            {
                Registros = registros.Values,
                Total = RecuperarTotalLinhas(sql.ToString(), dp)
            };

            return response;
        }

        public async Task<Medico?> RecuperarMedicoAsync(int codigoMedico, CancellationToken ct)
        {
            MedicosPaginacaoFiltro filtro = new() { Id = codigoMedico };
            PaginacaoConsulta<Medico> paginacaoConsulta = await ListarMedicosPaginadosAsync(filtro, ct);
            return paginacaoConsulta.Registros.FirstOrDefault();
        }
    }
}