using System.Text;
using Pacientes.Entidades;
using Pacientes.Repositorios;
using Usuarios.Request;
using Utils;
using Utils.DBContext;

namespace Pacientes
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