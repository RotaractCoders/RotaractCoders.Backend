using Domain.Commands.Inputs;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class Clube : TableEntity
    {
        public string Nome { get; private set; }
        public string Site { get; private set; }
        public string Facebook { get; private set; }
        public string Instagram { get; private set; }
        public string Email { get; private set; }
        public DateTime? DataFundacao { get; private set; }
        public string RotaryPadrinho { get; private set; }
        public DateTime? DataFechamento { get; private set; }
        public string NumeroDistrito { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        public Clube()
        {

        }

        public Clube(CriarClubeInput input)
        {
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            RotaryPadrinho = input.RotaryPadrinho;
            DataFechamento = input.DataFechamento;
            NumeroDistrito = input.NumeroDistrito;
            DataAtualizacao = DateTime.Now;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = NumeroDistrito;
        }

        public Clube Atualizar(Clube input)
        {
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            RotaryPadrinho = input.RotaryPadrinho;
            DataFechamento = input.DataFechamento;
            NumeroDistrito = input.NumeroDistrito;
            DataAtualizacao = DateTime.Now;

            PartitionKey = NumeroDistrito;

            return this;
        }

        public Clube Atualizar(CriarClubeInput input)
        {
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            RotaryPadrinho = input.RotaryPadrinho;
            DataFechamento = input.DataFechamento;
            NumeroDistrito = input.NumeroDistrito;
            DataAtualizacao = DateTime.Now;

            PartitionKey = NumeroDistrito;

            return this;
        }
    }
}
