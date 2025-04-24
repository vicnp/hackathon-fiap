using System.Text;
using Dapper;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;

namespace Hackathon.Fiap.Infra.HorariosDisponiveis
{

    public class HorariosDisponiveisRepositorio (DapperContext dapperContext) : RepositorioDapper<HorarioDisponivelConsulta>(dapperContext), IHorariosDisponiveisRepositorio
    {
        public async Task<HorarioDisponivel?> RecuperarHorarioDisponivel(int horarioDisponivelId, CancellationToken ct)
        {
            HorariosDisponiveisFiltro filtro = new() { HorarioDisponivelId = horarioDisponivelId };
            PaginacaoConsulta<HorarioDisponivelConsulta> paginacaoConsulta = await ListarHorariosDisponiveisAsync(filtro, ct);
            return paginacaoConsulta.Registros.Select(x => new HorarioDisponivel(x.HorarioDisponivelId, x.DataHoraInicio, x.DataHoraFim, x.Status)).FirstOrDefault();
        }

        public async Task<PaginacaoConsulta<HorarioDisponivelConsulta>> ListarHorariosDisponiveisAsync(HorariosDisponiveisFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
              @"select
	                hd.id                   AS HorarioDisponivelId,
	                hd.data_hora_inicio     AS DataHoraInicio,
	                hd.data_hora_fim        AS DataHoraFim,
	                hd.status               AS Status,
	                medico.id               AS MedicoId,
	                medico.nome             AS NomeMedico,
	                medico.cpf              AS CpfMedico,
	                medico.criado_em        AS CriadoEmMedico,
	                medico.email            AS EmailMedico,
	                medico.tipo             AS TipoMedico,
	                m.crm                   AS CrmMedico,
	                hd.paciente_id          AS PacienteId
                FROM techchallenge.Horario_Disponivel hd
                inner join techchallenge.Usuario medico on medico.Id = hd.medico_id
                inner join techchallenge.Medico m on m.id = medico.id
                where 1 = 1 ");


            if (filtro.HorarioDisponivelId is not null && filtro.HorarioDisponivelId > 0)
            {
                sql.AppendLine($" and hd.id = @ID ");
                dp.Add("@ID", filtro.HorarioDisponivelId);
            }

            if (filtro.MedicoId is not null && filtro.MedicoId > 0)
            {
                sql.AppendLine($" and medico.id = @MEDICOID ");
                dp.Add("@MEDICOID", filtro.MedicoId);
            }

            if (filtro.PacienteId is not null && filtro.PacienteId > 0)
            {
                sql.AppendLine($" and hd.paciente_id = @PACIENTEID ");
                dp.Add("@PACIENTEID", filtro.PacienteId);
            }

            if (filtro.Status is not null)
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
        
        public Task InserirHorariosDisponiveisAsync(IEnumerable<HorarioDisponivel> horarios, CancellationToken ct)
        {
            const string sql = @"
                INSERT INTO Horario_Disponivel (medico_id, data_hora_inicio, data_hora_fim, status)
                VALUES (@MedicoId, @DataHoraInicio, @DataHoraFim, @Status);";

            var parametros = horarios.Select(h => new
            {
                MedicoId = h.Medico.UsuarioId,
                h.DataHoraInicio,
                h.DataHoraFim,
                Status = h.Status.ToString(),
            });

            return session.ExecuteAsync(sql, parametros, commandTimeout: 30, transaction: null);
        }


        public Task AtualizarStatusHorarioDisponivel(StatusHorarioDisponivelEnum statusHorarioDisponivel, int horarioDisponivelId)
        {
            const string sql = @"
                UPDATE Horario_Disponivel
                   SET status = @Status
                  WHERE id = @Id;";

            var parametros = new
            {
                Status = statusHorarioDisponivel.ToString(),
                Id = horarioDisponivelId
            };

            return session.ExecuteAsync(sql, parametros, commandTimeout: 30, transaction: null);
        }

    }
}