using Domain.Commands.Inputs;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class Clube : TableEntity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Email { get; set; }
        public DateTime? DataFundacao { get; set; }
        public string RotaryPadrinho { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string NumeroDistrito { get; set; }
        public string Programa { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool BitAtivo { get; set; }

        public Clube()
        {

        }

        public Clube(CriarClubeInput input)
        {
            Codigo = input.Codigo;
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            RotaryPadrinho = input.RotaryPadrinho;
            DataFechamento = input.DataFechamento;
            NumeroDistrito = input.NumeroDistrito;
            Programa = input.Programa;
            DataAtualizacao = DateTime.Now;
            BitAtivo = true;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Codigo;
        }

        public Clube Atualizar(Clube input)
        {
            Codigo = input.Codigo;
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            RotaryPadrinho = input.RotaryPadrinho;
            DataFechamento = input.DataFechamento;
            NumeroDistrito = input.NumeroDistrito;
            Programa = input.Programa;
            DataAtualizacao = DateTime.Now;
            BitAtivo = input.BitAtivo;

            PartitionKey = Codigo;

            return this;
        }

        public Clube Atualizar(CriarClubeInput input)
        {
            Codigo = input.Codigo;
            Nome = input.Nome;
            Site = input.Site;
            Facebook = input.Facebook;
            Instagram = input.Instagram;
            Email = input.Email;
            DataFundacao = input.DataFundacao;
            RotaryPadrinho = input.RotaryPadrinho;
            DataFechamento = input.DataFechamento;
            NumeroDistrito = input.NumeroDistrito;
            Programa = input.Programa;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Codigo;

            return this;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
