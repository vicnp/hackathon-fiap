using System.Text;
using Dapper;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
using Hackathon.Fiap.Domain.Usuarios.Repositorios;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon.Fiap.Infra.Usuarios
{
    public class UsuariosRepositorio(DapperContext dapperContext) : RepositorioDapper<Usuario>(dapperContext), IUsuariosRepositorio
    {
        public Task<Usuario?> RecuperarUsuarioAsync(string identificador, string hash, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                     SELECT u.id as IdUsuario,
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

            DynamicParameters dp = new();
            dp.Add("identificador", identificador);
            dp.Add("hash", hash);

            return session.QueryFirstOrDefaultAsync<Usuario>(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<PaginacaoConsulta<Usuario>> ListarUsuariosAsync(UsuarioListarFiltro filtro, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                           SELECT u.id as IdUsuario,
		                          u.nome as Nome,
		                          u.email as Email,
		                          u.hash as Hash,
		                          u.criado_em as CriadoEm,
		                          u.tipo as Tipo
                    FROM techchallenge.Usuarios u
	                    WHERE 1 = 1");

            if (!filtro.Email.IsNullOrEmpty())
            {
                sql.AppendLine($@" AND u.email = '{filtro.Email}' ");
            }

            if (!filtro.NomeUsuario.IsNullOrEmpty())
            {
                sql.AppendLine($@" AND u.nome = '{filtro.NomeUsuario}' ");
            }

            return await ListarPaginadoAsync(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString(), ct: ct);
        }

        private async Task InserirUsuarioMedicoAsync(string crmMedico, int codigoUsuario, CancellationToken ct)
        {

            StringBuilder sql = new($@"
                                       INSERT INTO `Medicos` VALUES (@IDMEDICO,@CRM);
                                     ");

            DynamicParameters dp = new();

            dp.Add("@IDMEDICO", codigoUsuario);
            dp.Add("@CRM", crmMedico);

            await session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<Usuario> InserirUsuarioAsync(Usuario novoUsuario, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                      INSERT INTO `Usuarios`
                                     (nome, email, cpf, hash, tipo, criado_em)
                                      VALUES (@NOME,
                                              @EMAIL,
                                             @CPF,
                                             @HASH,
                                             @TIPO,
                                             @CRIADOEM
                                            );
                                     ");

            DynamicParameters dp = new();

            dp.Add("@CPF", novoUsuario.Cpf);
            dp.Add("@HASH", novoUsuario.Hash);
            dp.Add("@NOME", novoUsuario.Nome);
            dp.Add("@EMAIL", novoUsuario.Email);
            dp.Add("@HASH", novoUsuario.Cpf);
            dp.Add("@TIPO", novoUsuario.Tipo.ToString());
            dp.Add("@CRIADOEM", novoUsuario.CriadoEm);

            await session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));

            if (novoUsuario.Tipo == TipoUsuario.Medico)
                await InserirUsuarioMedicoAsync(novoUsuario.Cpf, novoUsuario.IdUsuario, ct);

            Usuario? result = await RecuperarUsuarioAsync(novoUsuario.Cpf, novoUsuario.Hash, ct);
            return result ?? throw new RegistroNaoEncontradoExcecao("Não foi possível cadastrar o usuário");
        }
    }
}
