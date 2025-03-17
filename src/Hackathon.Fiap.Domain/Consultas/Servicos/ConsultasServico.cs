using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Infra.Consultas.Consultas;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon.Fiap.Domain.Consultas.Servicos
{
    public class ConsultasServico(IConsultasRepositorio consultasRepositorio, IMedicosRepositorio medicosRepositorio, IPacientesRepositorio pacientesRepositorio) : IConsultaServico
    {
        public async Task<Consulta?> AtualizarStatusConsultaAsync(Consulta consulta, StatusConsultaEnum status, CancellationToken ct)
        {
            if (status is StatusConsultaEnum.Cancelada or StatusConsultaEnum.Recusada or StatusConsultaEnum.Aceita)
                ValidarCancelamentoRecusa(consulta);

            if (status is StatusConsultaEnum.Aceita && consulta.Status != StatusConsultaEnum.Pendente)
                throw new InvalidOperationException("A consulta não pode ser aceita.");

            consulta.Status = status;

            if (consulta.Status is StatusConsultaEnum.Cancelada)
                ValidarJustificativa(consulta);

            ConsultasListarFiltro filtro = new()
            {
                IdConsulta = consulta.IdConsulta,
            };

            await consultasRepositorio.AtualizarStatusConsultaAsync(consulta, ct);

            return await RecuperarConsultaAsync(filtro, ct);
        }

        private static void ValidarJustificativa(Consulta consulta)
        {
            if (consulta.JustificativaCancelamento.IsNullOrEmpty())
                throw new InvalidOperationException("Por favor forneça uma justificativa para o cancelamento.");
        }

        public static void ValidarCancelamentoRecusa(Consulta consulta)
        {
            switch (consulta.Status)
            {
                case StatusConsultaEnum.Cancelada:
                    throw new InvalidOperationException("A consulta está cancelada.");
                case StatusConsultaEnum.Recusada:
                    throw new InvalidOperationException("A consulta está recusada");
            }
        }

        public async Task<Consulta?> RecuperarConsultaAsync(ConsultasListarFiltro filtro, CancellationToken ct)
        {
            PaginacaoConsulta<Consulta> paginacaoConsulta = await ListarConsultasAsync(filtro, ct);
            ArgumentNullException.ThrowIfNull(paginacaoConsulta);
            if (paginacaoConsulta.Registros.Any())
                return paginacaoConsulta.Registros.FirstOrDefault();

            return null;
        }

        public async Task<PaginacaoConsulta<Consulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct)
        {
            PaginacaoConsulta<ConsultaConsulta> paginacaoConsulta = await consultasRepositorio.ListarConsultasAsync(filtro, ct);
            List<Consulta> consultas = [];
            PaginacaoConsulta<Consulta> response = new()
            {
                Total = paginacaoConsulta.Total,
                Registros = []
            };

            foreach (ConsultaConsulta itenConsulta in paginacaoConsulta.Registros)
            {
                if (itenConsulta.Status == null)
                    throw new InvalidCastException($"Não foi possível identificar a situação da consulta.");

                bool conversao = Enum.TryParse(itenConsulta.Status, out StatusConsultaEnum statusConsulta);

                if (!conversao)
                    throw new InvalidCastException("Não foi possivel determinar a situação da consulta.");

                Consulta consulta = new(itenConsulta.IdConsulta,
                                        itenConsulta.DataHora,
                                        itenConsulta.Valor,
                                        statusConsulta,
                                        itenConsulta.JustificativaCancelamento,
                                        itenConsulta.CriadoEm,
                                        itenConsulta.IdHorariosDisponiveis);

               
                Medico? medico = await medicosRepositorio.RecuperarMedico(itenConsulta.IdMedico, ct);
                Paciente? paciente = await pacientesRepositorio.RecuperarPaciente(itenConsulta.IdPaciente, ct);
                
                if(medico != null)
                    consulta.SetMedico(medico);

                if(paciente != null)
                    consulta.SetPaciente(paciente);

                consultas.Add(consulta);
            }
            response.Registros = consultas;
            return response;
        }
    }
}
