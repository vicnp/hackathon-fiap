using System.Text;
using Dapper;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;

namespace Hackathon.Fiap.Infra.Pacientes
{
    public class PacientesRepositorio(DapperContext dapperContext) : RepositorioDapper<Paciente>(dapperContext), IPacientesRepositorio
    {
        public PaginacaoConsulta<Paciente> ListarPacientes(UsuarioListarFiltro filtro)
        {
            DynamicParameters dp = new ();
            StringBuilder sql = new($@"
                                    SELECT id as IdUsuario,
                                           nome as Nome,
                                           email as Email,
                                           cpf as Cpf,
                                           hash as Hash,
                                           tipo as Tipo,
                                           criado_em as CriadoEm
                                    FROM techchallenge.Usuarios u
	                                WHERE u.tipo = 'Paciente'");

            if (!filtro.Email.InvalidOrEmpty())
            {
                sql.AppendLine($@" AND u.email = @EMAIL ");
                dp.Add("@EMAIL", filtro.Email);
            }

            if (!filtro.NomeUsuario.InvalidOrEmpty())
            {
                sql.AppendLine($@" AND u.nome = @NOME ");
                dp.Add("@NOME", filtro.NomeUsuario);
            }

            if (!filtro.Cpf.InvalidOrEmpty())
            {
                sql.AppendLine($@" AND u.cpf = @CPF ");
                dp.Add("@CPF", filtro.Cpf);
            }

            return ListarPaginado(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString(), dp);
        }

        public async Task<Paciente?> RecuperarPaciente(int idPaciente)
        {
            UsuarioListarFiltro filtro = new() { Id = idPaciente };
            PaginacaoConsulta<Paciente> paginacaoConsulta = ListarPacientes(filtro);
            return await Task.FromResult(paginacaoConsulta.Registros.FirstOrDefault());  
        }
    }
}