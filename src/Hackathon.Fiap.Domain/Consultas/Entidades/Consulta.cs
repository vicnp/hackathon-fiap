using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Domain.Consultas.Entidades
{
    public class Consulta
    {
        public int IdConsulta {  get; set; }
        public Medico Medico { get; set; } = new Medico();
        public Paciente Paciente { get; set; } = new Paciente();
        public DateTime DataHora { get; set; }
        public double Valor { get; set; }
        public StatusConsultaEnum Status { get; set; }
        public string JustificativaCancelamento { get; set; } = string.Empty;   
        public DateTime CriadoEm { get; set; }
        public int IdHorariosDisponiveis {get; set; }

        public Consulta()
        {
            
        }

        public Consulta(int idConsulta, DateTime dataHora, double valor, StatusConsultaEnum status, string justificativaCancelamento, DateTime criadoEm, int idHorariosDisponiveis)
        {
            IdConsulta = idConsulta;
            DataHora = dataHora;
            Valor = valor;
            Status = status;
            JustificativaCancelamento = justificativaCancelamento;
            CriadoEm = criadoEm;
            IdHorariosDisponiveis = idHorariosDisponiveis;
        }

        public void SetMedico(Medico medico)
        {
            Medico = medico;   
        }

        public void SetPaciente(Paciente paciente)
        {
            Paciente = paciente;
        }
    }
}
