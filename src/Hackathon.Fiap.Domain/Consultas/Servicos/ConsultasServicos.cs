using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Consultas.Repositorios;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Infra.Consultas.Consultas;

namespace Hackathon.Fiap.Domain.Consultas.Servicos
{
    public class ConsultasServicos(IConsultasRepositorio consultasRepositorio, IMedicosRepositorio medicosRepositorio, IPacientesRepositorio pacientesRepositorio) : IConsultaServico
    {
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

                Enum.TryParse(itenConsulta.Status, out StatusConsultaEnum statusConsulta);

                Consulta consulta = new(itenConsulta.IdConsulta,
                                        itenConsulta.DataHora,
                                        itenConsulta.Valor,
                                        statusConsulta,
                                        itenConsulta.JustificativaCancelamento,
                                        itenConsulta.CriadoEm,
                                        itenConsulta.IdHorariosDisponiveis);

               
                Medico? medico = await medicosRepositorio.RecuperarMedico(itenConsulta.IdMedico);
                Paciente? paciente = await pacientesRepositorio.RecuperarPaciente(itenConsulta.IdPaciente);
                
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
