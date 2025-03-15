using Dapper;
using System.Text;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;

namespace Hackathon.Fiap.Infra.Medicos
{
    public class MedicosRepositorio(DapperContext dapperContext) : RepositorioDapper<Medico>(dapperContext), IMedicosRepositorio
    {
        public async Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
                @"SELECT
	                    u.id as IdUsuario,
	                    u.cpf as Cpf,
	                    u.email as Email,
	                    u.nome as Nome,
	                    u.tipo as TipoUsuario,
                        m.crm as Crm,
	                    u.criado_em as CriadoEm,
	                    e.id as IdEspecialidade,
	                    e.nome as NomeEspecialidade,
	                    e.descricao as DescricaoEspecialidade
                    FROM
	                    techchallenge.Usuarios u
                    INNER JOIN techchallenge.Medicos m 
                    ON m.id = u.id 
                    INNER JOIN techchallenge.Medico_Especialidades me 
                    ON me.medico_id = u.id 
                    INNER JOIN techchallenge.Especialidades e 
                    ON e.id = me.especialidade_id 
                  WHERE 1 = 1");

            if (filtro.Id > 0)
            {
                sql.AppendLine($" AND u.id = @IDMEDICO ");
                dp.Add("@IDMEDICO", filtro.Id);
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
                sql.AppendLine($" AND e.id = @IDESPECIALIDADE ");
                dp.Add("@IDESPECIALIDADE", filtro.CodigoEspecialidade.Value);
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
                if (!registros.TryGetValue(medico.IdUsuario, out var existingMedico))
                {
                    existingMedico = medico;
                    registros[medico.IdUsuario] = existingMedico;
                }
                existingMedico.SetEspecialidade(especialidade);
                return existingMedico;
            }, splitOn: "IdEspecialidade", param: dp);

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
        public async Task<Medico?> RecuperarMedico(int codigoMedico, CancellationToken ct)
        {
            MedicosPaginacaoFiltro filtro = new() { Id = codigoMedico };
            PaginacaoConsulta<Medico> paginacaoConsulta = await ListarMedicosPaginadosAsync(filtro, ct);
            return paginacaoConsulta.Registros.FirstOrDefault();
        }
    }
}