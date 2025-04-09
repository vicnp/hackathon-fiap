using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Domain.Consultas.Entidades
{
    public class Consulta
    {
        public int ConsultaId {  get; set; }
        public Medico Medico { get; set; } = new Medico();
        public Paciente Paciente { get; set; } = new Paciente();
        public double Valor { get; set; }
        public StatusConsultaEnum Status { get; set; }
        public string JustificativaCancelamento { get; set; } = string.Empty;   
        public DateTime CriadoEm { get; set; }
        public HorarioDisponivel HorarioDisponivel { get; set; } = new HorarioDisponivel();

        public Consulta()
        {
            
        }

        public Consulta(
            double valor,
            StatusConsultaEnum status,
            Medico medico,
            HorarioDisponivel horarioDisponivel,
            Paciente paciente,
            string justificativaCancelamento)
        {
            Valor = valor;
            Status = status;
            CriadoEm = DateTime.Now;
            HorarioDisponivel = horarioDisponivel;
            Medico = medico;
            Paciente = paciente;
            JustificativaCancelamento = justificativaCancelamento;
        }

        public Consulta(
            int consultaId, 
            double valor, 
            StatusConsultaEnum status, 
            Medico medico,
            HorarioDisponivel horarioDisponivel,
            Paciente paciente,
            string justificativaCancelamento)
        {
            ConsultaId = consultaId;
            Valor = valor;
            Status = status;
            CriadoEm = DateTime.Now;
            HorarioDisponivel = horarioDisponivel;
            Medico = medico;
            Paciente = paciente;
            JustificativaCancelamento = justificativaCancelamento;
        }

        public void SetMedico(Medico medico)
        {
            Medico = medico;
        }

        public void SetPaciente(Paciente paciente)
        {
            Paciente = paciente;
        }

        public void SetHorarioDisponivel(HorarioDisponivel horarioDisponivel)
        {
            HorarioDisponivel = horarioDisponivel;
        }
    }
}
