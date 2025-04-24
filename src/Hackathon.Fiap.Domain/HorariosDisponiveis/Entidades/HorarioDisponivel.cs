using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Entidades
{
    public class HorarioDisponivel
    {
        public int HorarioDisponivelId {  get; set; }
        public Medico Medico { get; set; } = new Medico();
        public Paciente? Paciente { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public StatusHorarioDisponivelEnum Status { get; set; }

        public HorarioDisponivel()
        {
            
        }

        public HorarioDisponivel(int horarioDisponivelId, DateTime dataHoraInicio, DateTime dataHoraFim, StatusHorarioDisponivelEnum status)
        {
            HorarioDisponivelId = horarioDisponivelId;
            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;
            Status = status;
        }

        public void SetMedico(Medico medico)
        {
            Medico = medico;   
        }

        public void SetPaciente(Paciente? paciente)
        {
            Paciente = paciente;
        }
    }
}