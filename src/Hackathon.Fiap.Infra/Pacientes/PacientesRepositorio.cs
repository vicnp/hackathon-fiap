using System.Text;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;

namespace Hackathon.Fiap.Infra.Pacientes
{
    public class PacientesRepositorio(DapperContext dapperContext) : RepositorioDapper<Paciente>(dapperContext), IPacientesRepositorio
    {
        public PaginacaoConsulta<Paciente> ListarPacientes(UsuarioListarRequest request)
        {
            StringBuilder sql = new($@"
                                    SELECT id as Id,
                                           nome as Nome,
                                           email as Email,
                                           cpf as Cpf,
                                           hash as Hash,
                                           tipo as Tipo,
                                           criado_em as CriadoEm
                                    FROM techchallenge.Usuarios u
	                                WHERE u.tipo = 'Paciente'");

            if (!request.Email.InvalidOrEmpty())
            {
                sql.AppendLine($@" AND u.email = '{request.Email}' ");
            }

            if (!request.NomeUsuario.InvalidOrEmpty())
            {
                sql.AppendLine($@" AND u.nome = '{request.NomeUsuario}' ");
            }

            if (!request.Cpf.InvalidOrEmpty())
            {
                sql.AppendLine($@" AND u.cpf = '{request.Cpf}' ");
            }

            return ListarPaginado(sql.ToString(), request.Pg, request.Qt, request.CpOrd, request.TpOrd.ToString());
        }
    }
}