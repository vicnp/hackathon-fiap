using System.Text;
using Dapper;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
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

        public Task<Usuario?> RecuperarUsuarioPorIdAsync(int id, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                     SELECT u.id as IdUsuario,
                                            u.nome as Nome,
                                            u.email as Email,
                                            u.cpf as Cpf,
                                            u.hash as Hash,
                                            u.tipo as Tipo,
                                            u.criado_em as CriadoEm
                                     FROM techchallenge.Usuario u
                                     LEFT JOIN techchallenge.Medico m ON m.id = u.id
                        	         WHERE u.id = @ID ");

            DynamicParameters dp = new();
            dp.Add("ID", id);
            return session.QueryFirstOrDefaultAsync<Usuario>(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<PaginacaoConsulta<Usuario>> ListarUsuariosAsync(UsuarioListarFiltro filtro, CancellationToken ct)
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
                                       INSERT INTO `Medico` VALUES (@IDMEDICO,@CRM);
                                     ");

            DynamicParameters dp = new();

            dp.Add("@IDMEDICO", codigoUsuario);
            dp.Add("@CRM", crmMedico);

            await session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }

        public async Task<Usuario> InserirUsuarioAsync(Usuario novoUsuario, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                      INSERT INTO `Usuario`
                                     (nome, email, cpf, hash, tipo, criado_em)
                                      VALUES (@NOME,
                                              @EMAIL,
                                             @CPF,
                                             @HASH,
                                             @TIPO,
                                             @CRIADOEM
                                            );
                                    SELECT LAST_INSERT_ID();
                                     ");

            DynamicParameters dp = new();

            dp.Add("@NOME", novoUsuario.Nome);
            dp.Add("@EMAIL", novoUsuario.Email);
            dp.Add("@CPF", novoUsuario.Cpf);
            dp.Add("@HASH", novoUsuario.Hash);
            dp.Add("@TIPO", novoUsuario.Tipo.ToString());
            dp.Add("@CRIADOEM", novoUsuario.CriadoEm);

            int IdNovoUsuario = await session.ExecuteScalarAsync<int>(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));

            Usuario? result = await RecuperarUsuarioAsync(novoUsuario.Cpf, novoUsuario.Hash, ct);

            return result ?? throw new RegistroNaoEncontradoExcecao("Não foi possível cadastrar o usuário");
        }

        public async Task<Usuario> InserirUsuarioAsync(Medico novoMedico, CancellationToken ct)
        {
            Usuario novoUsuario = await InserirUsuarioAsync((Usuario)novoMedico, ct);
            await InserirUsuarioMedicoAsync(novoMedico.Crm, novoUsuario.UsuarioId, ct);
            return novoUsuario;
        }

        public Task DeletarUsuarioAsync(int id, CancellationToken ct)
        {
            StringBuilder sql = new($@"
                                       DELETE FROM techchallenge.Usuario
                                       WHERE id = @ID;

                                       DELETE FROM techchallenge.Medico
                                       WHERE Id = @ID;
                                     ");

            DynamicParameters dp = new();
            dp.Add("@ID", id);
            return session.ExecuteAsync(new CommandDefinition(sql.ToString(), dp, cancellationToken: ct));
        }
    }
}
