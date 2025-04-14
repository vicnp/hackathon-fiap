using System.Security.Policy;
using FluentAssertions;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Usuarios.Enumeradores;

namespace Hackathon.Fiap.Teste.Pacientes.Entidades;

public class PacienteTestes
{
    [Fact]
    public void Quando_CriarPaciente_DeveInicializarPropriedadesCorretamente()
    {
        // ARRANGE
        string nome = "João da Silva";
        string cpf = "12345678900";
        string email = "fiap@contato.com.br";
        string senha = "pastel de frango";
        string hash = "qwertyuiopasdfghjklzxcvbnm";

        // ACT
        var paciente = new Paciente(1, nome, email, cpf, hash, TipoUsuario.Paciente);

        // ASSERT
        paciente.Nome.Should().Be(nome);
        paciente.Cpf.Should().Be(cpf);
        paciente.Email.Should().Be(email);
        paciente.Tipo.Should().Be(TipoUsuario.Paciente);
    }

    [Fact]
    public void Quando_SetNome_ComValorValido_DeveAtualizarPropriedade()
    {
        // ARRANGE
        var paciente = new Paciente();
        var nome = "Maria da Silva";

        // ACT
        paciente.AtualizarNome(nome);

        // ASSERT
        paciente.Nome.Should().Be(nome);
    }
    

    [Fact]
    public void Quando_SetEmail_ComValorValido_DeveAtualizarPropriedade()
    {
        // ARRANGE
        var paciente = new Paciente();
        var email = "maria@email.com";

        // ACT
        paciente.AtualizarEmail(email);

        // ASSERT
        paciente.Email.Should().Be(email);
    }
} 