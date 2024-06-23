using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Contatos.Entidades;
using TC_Domain.Contatos.Repositorios;
using TC_Domain.Contatos.Repositorios.Filtros;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_DataTransfer.Contatos.Requests;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using TC_DataTransfer.Contatos.Requests;
using System.ComponentModel.DataAnnotations;

namespace TC_Domain.Contatos.Servicos
{
    public class ContatosServico(IContatosRepositorio contatosRepositorio) : IContatosServico
    {
        public PaginacaoConsulta<Contato> ListarContatos (ContatosFiltro request)
        {
            PaginacaoConsulta<Contato> consultaPaginada = contatosRepositorio.ListarContatos(request);
            return consultaPaginada;
        }

        public Contato InserirContato(ContatoInserirRequest contatoRequest)
        {
            ValidarCampos(contatoRequest);

            Contato contatoInserir = new(contatoRequest.Nome!,contatoRequest.Email!, (int)contatoRequest.DDD!, contatoRequest.Telefone!);

            Contato response = contatosRepositorio.InserirContato(contatoInserir);
            return response;
        }

        private static void ValidarCampos(ContatoInserirRequest contatoRequest)
        {
            if (contatoRequest.Nome.IsNullOrEmpty())
                throw new ArgumentException("Nome não preenchido.");

            if (contatoRequest.Email.IsNullOrEmpty())
                throw new ArgumentException("Email não preenchido.");

            if (!ValidarEmail(contatoRequest.Email))
                throw new ArgumentException($"Email inválido: {contatoRequest.Email}");

            if (contatoRequest.Telefone.IsNullOrEmpty())
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
