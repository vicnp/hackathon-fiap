using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Hackathon.Fiap.Infra.Consultas.Consultas;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon.Fiap.Domain.Consultas.Servicos
{
    public class ConsultasServico(
        IConsultasRepositorio consultasRepositorio, 
        IMedicosServico medicosServico,
        IHorariosDisponiveisServico horariosDisponiveisServico,
        IHorariosDisponiveisRepositorio horariosDisponiveisRepositorio,
        IPacientesServicos pacientesServicos) : IConsultasServico
    {
        public async Task<Consulta?> AtualizarStatusConsultaAsync(Consulta consulta, StatusConsultaEnum status, CancellationToken ct)
        {
            if (status is StatusConsultaEnum.Cancelada or StatusConsultaEnum.Recusada or StatusConsultaEnum.Aceita)
                ValidarCancelamentoRecusa(consulta);

            if (status is StatusConsultaEnum.Aceita && consulta.Status != StatusConsultaEnum.Pendente)
                throw new RegraDeNegocioExcecao("A consulta não pode ser aceita.");

            consulta.Status = status;

            if (consulta.Status is StatusConsultaEnum.Cancelada)
                ValidarJustificativa(consulta);

            ConsultasListarFiltro filtro = new()
            {
                ConsultaId = consulta.ConsultaId,
            };

            await consultasRepositorio.AtualizarStatusConsultaAsync(consulta, ct);

            return await RecuperarConsultaAsync(filtro, ct);
        }

        private static void ValidarJustificativa(Consulta consulta)
        {
            if (consulta.JustificativaCancelamento.IsNullOrEmpty())
                throw new RegraDeNegocioExcecao("Por favor forneça uma justificativa para o cancelamento.");
        }

        public static void ValidarCancelamentoRecusa(Consulta consulta)
        {
            switch (consulta.Status)
            {
                case StatusConsultaEnum.Cancelada:
                    throw new RegraDeNegocioExcecao("A consulta está cancelada.");
                case StatusConsultaEnum.Recusada:
                    throw new RegraDeNegocioExcecao("A consulta está recusada");
            }
        }

        public async Task<Consulta?> RecuperarConsultaAsync(ConsultasListarFiltro filtro, CancellationToken ct)
        {
            PaginacaoConsulta<Consulta> paginacaoConsulta = await ListarConsultasAsync(filtro, ct);
            RegraDeNegocioExcecao.LancarExcecaoSeNulo(paginacaoConsulta);
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

            foreach (ConsultaConsulta itemConsulta in paginacaoConsulta.Registros)
            {
                if (itemConsulta.Status == null)
                    throw new RegraDeNegocioExcecao($"Não foi possível identificar a situação da consulta.");

                bool conversao = Enum.TryParse(itemConsulta.Status, out StatusConsultaEnum statusConsulta);

                if (!conversao)
                    throw new FalhaConversaoExcecao("Não foi possivel determinar a situação da consulta.");

                Consulta consulta = new(itemConsulta.ConsultaId,
                                        itemConsulta.Valor,
                                        statusConsulta,
                                        await medicosServico.ValidarMedicoAsync(itemConsulta.MedicoId, ct),
                                        await horariosDisponiveisServico.ValidarHorarioDisponivelAsync(itemConsulta.HorarioDisponivelId, ct),
                                        await pacientesServicos.ValidarPacienteAsync(itemConsulta.PacienteId, ct),
                                        itemConsulta.JustificativaCancelamento);
               
                consultas.Add(consulta);
            }
            response.Registros = consultas;
            return response;
        }

        public async Task<Consulta> InserirConsultaAsync(ConsultaInserirFiltro filtro, CancellationToken ct)
        {
            Medico medico = await medicosServico.ValidarMedicoAsync(filtro.MedicoId, ct);
            Paciente paciente = await pacientesServicos.ValidarPacienteAsync(filtro.PacienteId, ct);
            HorarioDisponivel horarioDisponivel = await horariosDisponiveisServico.ValidarHorarioDisponivelAsync(filtro.HorarioDisponivelId, ct);

            if (horarioDisponivel.Status != StatusHorarioDisponivelEnum.Disponivel)
                throw new RegraDeNegocioExcecao("O horário selecionado não está mais disponível!");

            await horariosDisponiveisRepositorio.AtualizarStatusHorarioDisponivel(StatusHorarioDisponivelEnum.Reservado, horarioDisponivel.HorarioDisponivelId);

            Consulta consulta = new(
                filtro.Valor,
                filtro.Status,
                medico,
                horarioDisponivel,
                paciente,
                string.Empty);
          return await consultasRepositorio.InserirConsultaAsync(consulta, ct);
        }
    }
}
