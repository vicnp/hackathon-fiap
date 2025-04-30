using FluentAssertions;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Servicos;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using NSubstitute;

namespace Hackathon.Fiap.Teste.Medicos.Servicos
{
    public class EspecialidadesServicoTestes
    {
        private readonly IEspecialidadesRepositorio especialidadesRepositorio;
        private readonly EspecialidadesServico especialidadesServico;

        public EspecialidadesServicoTestes()
        {
            especialidadesRepositorio = Substitute.For<IEspecialidadesRepositorio>();
            especialidadesServico = new EspecialidadesServico(especialidadesRepositorio);
        }

        [Fact]
        public async Task Quando_GetPacientelValido_Espero_ObjException()
        {
            Especialidade especialidade = new("123", "asd");
            especialidadesRepositorio.RecuperarEspecialidadeAsync(1, CancellationToken.None).Returns(especialidade);
            Func<Task> act = async () => await especialidadesServico.ValidarEspecialidadeAsync(1, CancellationToken.None);
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Quando_GetPacientelInvalido_Espero_ObjException()
        {
            await FluentActions.Awaiting(() => especialidadesServico.ValidarEspecialidadeAsync(Arg.Any<int>(), Arg.Any<CancellationToken>()))
                .Should().ThrowAsync<RegistroNaoEncontradoExcecao>()
                .WithMessage("Especialidade não encontrada!");
        }
    }
}