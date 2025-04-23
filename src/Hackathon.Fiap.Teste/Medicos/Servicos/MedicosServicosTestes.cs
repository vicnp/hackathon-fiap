using FluentAssertions;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Servicos;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Servicos;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Threading;

namespace Hackathon.Fiap.Teste.Medicos.Servicos
{
    public class MedicosServicosTestes
    {
        private readonly IMedicosRepositorio medicosRepositorio;
        private readonly MedicosServico medicosServico;

        public MedicosServicosTestes()
        {
            medicosRepositorio = Substitute.For<IMedicosRepositorio>();
            medicosServico = new MedicosServico(medicosRepositorio);
        }

        [Fact]
        public async Task Quando_GetPacientelValido_Espero_ObjException()
        {
            Medico medico = new();
            medicosRepositorio.RecuperarMedicoAsync(1, CancellationToken.None).Returns(medico);
            Func<Task> act = async () => await medicosServico.ValidarMedicoAsync(1, CancellationToken.None);
            await act.Should().NotThrowAsync();

        }

        [Fact]
        public async Task Quando_GetPacientelInvalido_Espero_ObjException()
        {
            await FluentActions.Awaiting(() => medicosServico.ValidarMedicoAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()))
                .Should().ThrowAsync<RegistroNaoEncontradoExcecao>()
                .WithMessage("Médico não encontrado!");
        }
    }
}
