﻿using System.Text;
using Dapper;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Helpers;
using Hackathon.Fiap.Infra.Utils;
using Hackathon.Fiap.Infra.Utils.DBContext;

namespace Hackathon.Fiap.Infra.Pacientes
{
    public class PacientesRepositorio(DapperContext dapperContext) : RepositorioDapper<Paciente>(dapperContext), IPacientesRepositorio
    {
        public async Task<PaginacaoConsulta<Paciente>> ListarPacientesAsync(UsuarioListarFiltro filtro, CancellationToken ct)
        {
            DynamicParameters dp = new ();
            StringBuilder sql = new($@"
                                    SELECT id as UsuarioId,
                                           nome as Nome,
                                           email as Email,
                                           cpf as Cpf,
                                           hash as Hash,
                                           tipo as Tipo,
                                           criado_em as CriadoEm
                                    FROM techchallenge.Usuario u
	                                WHERE u.tipo = 'Paciente'");

            if (filtro.Id > 0)
            {
                sql.AppendLine($@" AND u.id = @ID ");
                dp.Add("@ID", filtro.Id);
            }
            
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
            PaginacaoConsulta<Paciente> paginacaoConsulta = await ListarPaginadoAsync(sql.ToString(), filtro.Pg, filtro.Qt, filtro.CpOrd, filtro.TpOrd.ToString(), dp, ct);
            return paginacaoConsulta;
        }

        public async Task<Paciente?> RecuperarPaciente(int idPaciente, CancellationToken ct)
        {            
            UsuarioListarFiltro filtro = new() { Id = idPaciente };
            PaginacaoConsulta<Paciente> paginacaoConsulta = await ListarPacientesAsync(filtro, ct);
            return paginacaoConsulta.Registros.FirstOrDefault();
        }
    }
}