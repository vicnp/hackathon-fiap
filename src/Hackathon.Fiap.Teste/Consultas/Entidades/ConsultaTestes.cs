using FluentAssertions;
using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Teste.Consultas.Entidades;

public class ConsultaTestes
{
    [Fact]
    public void Quando_CriarConsulta_ComMedicoEPaciente_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        int id = 1;
        DateTime dataHora = new DateTime(2025, 03, 31, 9, 0, 0);
        double valor = 150.00;
        StatusConsultaEnum status = StatusConsultaEnum.Pendente;
        string justificativaCancelamento = "";
        DateTime criadoEm = DateTime.Now;
        int idHorariosDisponiveis = 1;
        
        // Criando a especialidade
        var especialidade = new Especialidade { IdEspecialidade = 1, NomeEspecialidade = "Cardiologia", DescricaoEspecialidade = "Especialidade de coração" };
        
        // Criando o médico com a especialidade e CRM
        var medico = new Medico();
        medico.SetCrm("123456");
        medico.SetEspecialidade(especialidade);

        // Criando o paciente
        var paciente = new Paciente();

        // ACT
        var consulta = new Consulta(id, dataHora, valor, status, justificativaCancelamento, criadoEm, idHorariosDisponiveis);
        consulta.SetMedico(medico);
        consulta.SetPaciente(paciente);

        // ASSERT
        consulta.Medico.Should().Be(medico);
        consulta.Medico.Crm.Should().Be("123456");
        consulta.Medico.Especialidade.Should().Be(especialidade);
        consulta.Paciente.Should().Be(paciente);
        consulta.DataHora.Should().Be(dataHora);
        consulta.Valor.Should().Be(valor);
        consulta.Status.Should().Be(status);
        consulta.JustificativaCancelamento.Should().Be(justificativaCancelamento);
        consulta.CriadoEm.Should().Be(criadoEm);
        consulta.IdHorariosDisponiveis.Should().Be(idHorariosDisponiveis);
    }

    [Fact]
    public void Quando_CriarConsulta_ComStatusCancelada_DeveInicializarComJustificativa()
    {
        // ARRANGE
        int id = 1;
        DateTime dataHora = new DateTime(2025, 03, 31, 9, 0, 0);
        double valor = 150.00;
        StatusConsultaEnum status = StatusConsultaEnum.Cancelada;
        string justificativaCancelamento = "Paciente solicitou cancelamento";
        DateTime criadoEm = DateTime.Now;
        int idHorariosDisponiveis = 1;

        // ACT
        var consulta = new Consulta(id, dataHora, valor, status, justificativaCancelamento, criadoEm, idHorariosDisponiveis);

        // ASSERT
        consulta.Status.Should().Be(StatusConsultaEnum.Cancelada);
        consulta.JustificativaCancelamento.Should().Be(justificativaCancelamento);
    }

    [Fact]
    public void Quando_SetMedicoEPaciente_DeveAtualizarPropriedadesCorretamente()
    {
        // ARRANGE
        var consulta = new Consulta();
        var medico = new Medico();
        var paciente = new Paciente();

        // ACT
        consulta.SetMedico(medico);
        consulta.SetPaciente(paciente);

        // ASSERT
        consulta.Medico.Should().Be(medico);
        consulta.Paciente.Should().Be(paciente);
    }
} 