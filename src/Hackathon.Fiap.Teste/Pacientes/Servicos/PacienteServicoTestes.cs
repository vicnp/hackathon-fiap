using FluentAssertions;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Servicos;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;

namespace Hackathon.Fiap.Teste.Pacientes.Servicos
{
    public class PacienteServicoTestes
    {

        private readonly IPacientesRepositorio pacientesRepositorio;
        private readonly PacienteServico pacienteServico;

        public PacienteServicoTestes()
        {
            pacientesRepositorio = Substitute.For<IPacientesRepositorio>();
            pacienteServico = new PacienteServico(pacientesRepositorio);
        }

        [Fact]
        public async Task Quando_GetPacientelValido_Espero_ObjException()
        {
            Paciente paciente = new();
            pacientesRepositorio.RecuperarPaciente(1, CancellationToken.None).Returns(paciente);
            Func<Task> act = async () => await pacienteServico.ValidarPacienteAsync(1, CancellationToken.None);
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Quando_GetPacientelInvalido_Espero_ObjException()
        {
            await FluentActions.Awaiting(() => pacienteServico.ValidarPacienteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()))
                .Should().ThrowAsync<RegistroNaoEncontradoExcecao>()
                .WithMessage("Paciente não encontrado!");
        }
    }
}
