using Dapper;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hackathon.Fiap.Infra.Usuarios
{
    public class UsuariosRepositorio(DapperContext dapperContext) : RepositorioDapper<Usuario>(dapperContext), IUsuariosRepositorio
    {
        public Task<Usuario?> RecuperarUsuarioAsync(string identificador, string hash, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                     SELECT u.id as UsuarioId,
                                            u.nome as Nome,
                                            u.email as Email,
                                            u.cpf as Cpf,
                                            u.hash as Hash,
                                            u.tipo as Tipo,
                                            u.criado_em as CriadoEm
                                     FROM techchallenge.Usuario u
                                     LEFT JOIN techchallenge.Medico m ON m.id = u.id
                        	         WHERE u.hash = @hash
                        	         AND (u.email = @identificador OR u.cpf = @identificador OR m.crm = @identificador)");

            DynamicParameters dp = new();
            dp.Add("identificador", identificador);
            dp.Add("hash", hash);

            return session.QueryFirstOrDefaultAsync<Usuario>(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<PaginacaoConsulta<Usuario>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                           SELECT u.id as UsuarioId,
		                          u.nome as Nome,
		                          u.email as Email,
		                          u.hash as Hash,
		                          u.criado_em as CriadoEm,
		                          u.tipo as Tipo
                    FROM techchallenge.Usuario u
	                    WHERE 1 = 1");

            if (!request.Email.IsNullOrEmpty())
            {
                sql.AppendLine($@" AND u.email = '{request.Email}' ");
            }

            if (!request.NomeUsuario.IsNullOrEmpty())
            {
                sql.AppendLine($@" AND u.nome = '{request.NomeUsuario}' ");
            }

            return await ListarPaginadoAsync(sql.ToString(), request.Pg, request.Qt, request.CpOrd, request.TpOrd.ToString(), ct: ct);
        }
    }
}
