using System.Text;
using Dapper;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;

namespace Hackathon.Fiap.Infra.HorariosDisponiveis
{

    public class HorariosDisponiveisRepositorio (DapperContext dapperContext) : RepositorioDapper<HorarioDisponivelConsulta>(dapperContext), IHorariosDisponiveisRepositorio
    {
        public async Task<PaginacaoConsulta<HorarioDisponivelConsulta>> ListarHorariosDisponiveisAsync(HorariosDisponiveisFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
              @"select
	                hd.id                   AS IdHorarioDisponivel,
	                hd.data_hora_inicio     AS DataHoraInicio,
	                hd.data_hora_fim        AS DataHoraFim,
	                hd.status               AS Status,
	                medico.id               AS IdMedico,
	                medico.nome             AS NomeMedico,
	                medico.cpf              AS CpfMedico,
	                medico.criado_em        AS CriadoEmMedico,
	                medico.email            AS EmailMedico,
	                medico.tipo             AS TipoMedico,
	                m.crm                   AS CrmMedico,
	                hd.paciente_id          AS IdPaciente
                FROM techchallenge.Horarios_Disponiveis hd
                inner join techchallenge.Usuarios medico on medico.Id = hd.medico_id
                inner join techchallenge.Medicos m on m.id = medico.id
                where 1 = 1 ");

            if (filtro.IdMedico > 0)
            {
                sql.AppendLine($" and medico.id = @IDMEDICO ");
                dp.Add("@IDMEDICO", filtro.IdMedico);
            }

            if (filtro.IdPaciente > 0)
            {
                sql.AppendLine($" and hd.paciente_id = @IDPACIENTE ");
                dp.Add("@IDPACIENTE", filtro.IdPaciente);
            }

            if (filtro.Status > 0)
            {
                sql.AppendLine($" and hd.status = @STATUS ");
                dp.Add("@STATUS", filtro.Status.ToString());
            }

            string sqlPaginado = GerarQueryPaginacao(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString());
            
            
            IEnumerable<HorarioDisponivelConsulta> queryResult = await session.QueryAsync<HorarioDisponivelConsulta>(sqlPaginado, dp);

            PaginacaoConsulta<HorarioDisponivelConsulta> response = new()
            {
                Registros = queryResult,
                Total = RecuperarTotalLinhas(sql.ToString(), dp)
            };
            
            return response;
        }
        
        public async Task InserirHorariosDisponiveisAsync(IEnumerable<HorarioDisponivel> horarios, CancellationToken ct)
        {
            const string sql = @"
                INSERT INTO Horarios_Disponiveis (medico_id, data_hora_inicio, data_hora_fim, status)
                VALUES (@MedicoId, @DataHoraInicio, @DataHoraFim, @Status);";

            var parametros = horarios.Select(h => new
            {
                MedicoId = h.Medico.IdUsuario,
                DataHoraInicio = h.DataHoraInicio,
                DataHoraFim = h.DataHoraFim,
                Status = h.Status.ToString(),
            });

            await session.ExecuteAsync(sql, parametros, commandTimeout: 30, transaction: null);
        }

    }
}