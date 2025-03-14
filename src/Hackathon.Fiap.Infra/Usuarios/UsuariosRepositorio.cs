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
        public Usuario RecuperarUsuario(string identificador, string hash)
        {
            StringBuilder sql = new($@"
                                     SELECT u.id as Id,
                                            u.nome as Nome,
                                            u.email as Email,
                                            u.cpf as Cpf,
                                            u.hash as Hash,
                                            u.tipo as Tipo,
                                            u.criado_em as CriadoEm
                                     FROM techchallenge.Usuarios u
                                     LEFT JOIN techchallenge.Medicos m ON m.id = u.id
                        	         WHERE u.hash = @hash
                        	         AND (u.email = @identificador OR u.cpf = @identificador OR m.crm = @identificador)");

            DynamicParameters parametros = new();
            parametros.Add("identificador", identificador);
            parametros.Add("hash", hash);

            return session.QueryFirstOrDefault<Usuario>(sql.ToString(), parametros);
        }



        public PaginacaoConsulta<Usuario> ListarUsuarios(UsuarioListarRequest request)
        {
            StringBuilder sql = new($@"
                           SELECT u.id as Id,
		                    u.nome as Nome,
		                    u.email as Email,
		                    u.hash as Hash,
		                    u.data_criacao as DataCriacao,
		                    u.permissao as Permissao
                    FROM techchallenge.usuarios u
	                    WHERE 1 = 1");

            if (!request.Email.IsNullOrEmpty())
            {
                sql.AppendLine($@" AND u.email = '{request.Email}' ");
            }

            if (!request.NomeUsuario.IsNullOrEmpty())
            {
                sql.AppendLine($@" AND u.nome = '{request.NomeUsuario}' ");
            }

            return ListarPaginado(sql.ToString(), request.Pg, request.Qt, request.CpOrd, request.TpOrd.ToString());
        }
    }
}
