using System.ComponentModel.DataAnnotations;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_Domain.Utils;

namespace TC_Domain.Contatos.Servicos
{
    public class ContatosServico(IContatosRepositorio contatosRepositorio) : IContatosServico
    {
        public async Task<PaginacaoConsulta<Contato>> ListarPaginacaoContatosAsync (ContatosPaginadosFiltro request)
        {
            PaginacaoConsulta<Contato> consultaPaginada = await contatosRepositorio.ListarPaginacaoContatosAsync(request);
            return consultaPaginada;
        }

        public async Task<List<Contato>> ListarContatosAsync(ContatoFiltro request)
            => await contatosRepositorio.ListarContatosAsync(request);


        public async Task<Contato> InserirContatoAsync(ContatoFiltro novoContato)
        {
            ValidarCampos(novoContato);

            Contato contatoInserir = new(novoContato.Nome!,novoContato.Email!, (int)novoContato.DDD!, novoContato.Telefone!);

            Contato response = await contatosRepositorio.InserirContatoAsync(contatoInserir);
            return response;
        }

        public async Task RemoverContatoAsync(int id)
        {
            Contato contato = await RecuperarContatoAsync(id);
            if (contato != null) 
                await contatosRepositorio.RemoverContatoAsync((int)contato.Id!);

        }

        public async Task<Contato> RecuperarContatoAsync(int id)
        {
           return await contatosRepositorio.RecuperarContatoAsync(id);
        }

        public async Task<Contato> AtualizarContatoAsync(Contato contato)
        {
            return await contatosRepositorio.AtualizarContatoAsync(contato);
        }

        private static void ValidarCampos(ContatoFiltro contatoRequest)
        {
            if (string.IsNullOrEmpty(contatoRequest.Nome))
                throw new ArgumentException("Nome não preenchido.");

            if (string.IsNullOrEmpty(contatoRequest.Email))
                throw new ArgumentException("Email não preenchido.");

            if (!ValidarEmail(contatoRequest.Email))
                throw new ArgumentException($"Email inválido: {contatoRequest.Email}");

            if (string.IsNullOrEmpty(contatoRequest.Telefone))
                throw new ArgumentException("Telefone não preenchido.");

            if (!contatoRequest.Telefone!.All(char.IsDigit))
                throw new ArgumentException($"Telefone inválido: {contatoRequest.Telefone}");

            if (contatoRequest.DDD == null || contatoRequest.DDD <= 0)
                throw new ArgumentException("DDD não preenchido.");
        }

        public static bool ValidarEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (new EmailAddressAttribute().IsValid(email))
                return true;

            return false;
        }

    }
}
