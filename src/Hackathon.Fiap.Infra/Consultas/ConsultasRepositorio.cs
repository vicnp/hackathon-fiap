using System.Text;
using Dapper;
using Hackathon.Fiap.Domain.Consultas.Consultas;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Microsoft.IdentityModel.Tokens;
using static Dapper.SqlMapper;

namespace Hackathon.Fiap.Infra.Consultas
{
    public class ConsultasRepositorio (DapperContext dapperContext) : RepositorioDapper<ConsultaConsulta>(dapperContext), IConsultasRepositorio
    {
        public async Task<int> AtualizarStatusConsultaAsync(Consulta consulta, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
              @"
                UPDATE
	                techchallenge.Consulta
                SET
	                status = @STATUS ");

            if (!consulta.JustificativaCancelamento.IsNullOrEmpty())
            {
                sql.Append(@", justificativa_cancelamento = @JUSTIFICATIVA ");
                
            }
            sql.Append(@" WHERE
	                       id = @CONSULTAID;
                        ");

            dp.Add("@CONSULTAID", consulta.ConsultaId);
            dp.Add("@STATUS", consulta.Status.ToString());
            dp.Add("@JUSTIFICATIVA", consulta.JustificativaCancelamento?.ToLower());

            return await session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<PaginacaoConsulta<ConsultaConsulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
              @"select
	                c.id as ConsultaId,
	                c.valor as Valor,
	                c.status as Status,
	                c.justificativa_cancelamento as JustificativaCancelamento,
	                c.criado_em as CriadoEm,
	                c.horario_disponivel_id as HorarioDisponivelId,
	                medico.id as MedicoId,
	                medico.nome as NomeMedico,
	                medico.cpf as CpfMedico,
	                medico.criado_em as CriadoEmMedico,
	                medico.email EmailMedico,
	                medico.tipo as TipoMedico,
	                m.crm as CrmMedico,
	                paciente.id  as PacienteId,
	                paciente.nome as NomePaciente,
	                paciente.email as EmailPaciente,
                    paciente.cpf as CpfPaciente,
	                paciente.criado_em as CriadoEmPaciente
                from
	                techchallenge.Consulta c
                inner join techchallenge.Usuario medico
                on
	                medico.Id = c.medico_id
                inner join techchallenge.Medico m 
                on 
	                m.id = medico.id 
                inner join techchallenge.Usuario paciente 
                on 
	                paciente.id = c.paciente_id
                where 1 = 1 ");

            if (filtro.MedicoId > 0)
            {
                sql.AppendLine($" and medico.id = @MEDICOID ");
                dp.Add("@MEDICOID", filtro.MedicoId);
            }

            if (filtro.PacienteId > 0)
            {
                sql.AppendLine($" and paciente.id = @PACIENTEID ");
                dp.Add("@PACIENTEID", filtro.PacienteId);
            }

            if (filtro.HorarioDisponivelId > 0)
            {
                sql.AppendLine($" and c.horario_disponivel_id = @HORARIODISPONIVELID ");
                dp.Add("@HORARIODISPONIVELID", filtro.HorarioDisponivelId);
            }

            if (filtro.Status != null && filtro.Status != 0)
            {
                sql.AppendLine($" and c.Status = @STATUS ");
                dp.Add("@STATUS", filtro.Status.ToString());
            }

            if(filtro.ConsultaId > 0)
            {
                sql.AppendLine($" and c.id = @CONSULTAID ");
                dp.Add("@CONSULTAID", filtro.ConsultaId);
            }

            string sqlPaginado = GerarQueryPaginacao(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString());
            
            IEnumerable<ConsultaConsulta> queryResult = await session.QueryAsync<ConsultaConsulta>(new CommandDefinition(sqlPaginado, dp, cancellationToken: ct));

            PaginacaoConsulta<ConsultaConsulta> response = new()
            {
                Registros = queryResult,
                Total = RecuperarTotalLinhas(sql.ToString(), dp)
            };
            
            return response;
        }


        public async Task<Consulta> InserirConsultaAsync(Consulta consulta, CancellationToken ct)
        {
            const string sql = @"
                INSERT INTO Consulta (paciente_id, medico_id, valor, status, horario_disponivel_id, criado_em)
                    VALUES (@PacienteId, @MedicoId, @Valor, @Status, @HorariosDisponiveisId, @Criado);
                SELECT LAST_INSERT_ID();";

            var parametros = new
            {
                PacienteId = consulta.Paciente.UsuarioId,
                MedicoId = consulta.Medico.UsuarioId,
                consulta.Valor,
                Status = consulta.Status.ToString(),
                HorariosDisponiveisId = consulta.HorarioDisponivel.HorarioDisponivelId,
                Criado = consulta.CriadoEm
            };

            var id = await session.QueryFirstOrDefaultAsync<int>(
                new CommandDefinition(sql, parametros, commandTimeout: 30, transaction: null, cancellationToken: ct)
            );

            consulta.ConsultaId = id; 
            return consulta;
        }

    }
}
