using System.Text;
using Dapper;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Infra.Consultas.Consultas;
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
	                techchallenge.Consultas
                SET
	                status = @STATUS ");

            if (!consulta.JustificativaCancelamento.IsNullOrEmpty())
            {
                sql.Append(@", justificativa_cancelamento = @JUSTIFICATIVA ");
                
            }
            sql.Append(@" WHERE
	                       id = @IDCONSULTA;
                        ");

            dp.Add("@IDCONSULTA", consulta.IdConsulta);
            dp.Add("@STATUS", consulta.Status.ToString());
            dp.Add("@JUSTIFICATIVA", consulta.JustificativaCancelamento?.ToLower());

            return await session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<PaginacaoConsulta<ConsultaConsulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new();
            StringBuilder sql = new(
              @"select
	                c.id as IdConsulta,
	                c.data_hora as DataHora,
	                c.valor as Valor,
	                c.status as Status,
	                c.justificativa_cancelamento as JustificativaCancelamento,
	                c.criado_em as CriadoEm,
	                c.horarios_disponiveis_id as IdHorariosDisponiveis,
	                medico.id as IdMedico,
	                medico.nome as NomeMedico,
	                medico.cpf as CpfMedico,
	                medico.criado_em as CriadoEmMedico,
	                medico.email EmailMedico,
	                medico.tipo as TipoMedico,
	                m.crm as CrmMedico,
	                paciente.id  as IdPaciente,
	                paciente.nome as NomePaciente,
	                paciente.email as EmailPaciente,
                    paciente.cpf as CpfPaciente,
	                paciente.criado_em as CriadoEmPaciente
                from
	                techchallenge.Consultas c
                inner join techchallenge.Usuarios medico
                on
	                medico.Id = c.medico_id
                inner join techchallenge.Medicos m 
                on 
	                m.id = medico.id 
                inner join techchallenge.Usuarios paciente 
                on 
	                paciente.id = c.paciente_id
                where 1 = 1 ");

            if (filtro.IdMedico > 0)
            {
                sql.AppendLine($" and medico.id = @IDMEDICO ");
                dp.Add("@IDMEDICO", filtro.IdMedico);
            }

            if (filtro.IdPaciente > 0)
            {
                sql.AppendLine($" and paciente.id = @IDPACIENTE ");
                dp.Add("@IDPACIENTE", filtro.IdPaciente);
            }

            if(filtro.IdPaciente > 0)
            {
                sql.AppendLine($" and c.Id = @IDCONSULTA ");
                dp.Add("@IDCONSULTA", filtro.IdPaciente);
            }

            if(filtro.Status != null && filtro.Status != 0)
            {
                sql.AppendLine($" and c.Status = @STATUS ");
                dp.Add("@STATUS", filtro.Status.ToString());
            }

            if(filtro.IdConsulta > 0)
            {
                sql.AppendLine($" and c.id = @IDCONSULTA ");
                dp.Add("@IDCONSULTA", filtro.IdConsulta);
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
    }
}
