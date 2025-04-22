using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros
{
    public class ConsultaInserirFiltro
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int HorarioDisponivelId { get; set; }
        public StatusConsultaEnum Status { get; set; }
        public double Valor { get; set; }
        public ConsultaInserirFiltro()
        {
            
        }
        public ConsultaInserirFiltro(int medicoId, int pacienteId, int horarioDisponivelId, StatusConsultaEnum status, double valor)
        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            HorarioDisponivelId = horarioDisponivelId;
            Status = status;
            Valor = valor;
        }
    }
}
