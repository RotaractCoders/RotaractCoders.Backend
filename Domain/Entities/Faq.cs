using Domain.Commands.Inputs;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class Faq : TableEntity
    {
        public int Posicao { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool BitAtivo { get; set; } = true;

        public Faq()
        {

        }

        public Faq(IncluirFaqInput input)
        {
            Pergunta = input.Pergunta;
            Resposta = input.Resposta;
            Posicao = input.Posicao;
            DataAtualizacao = DateTime.Now;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Pergunta;
        }

        public void Atualizar(IncluirFaqInput input)
        {
            Pergunta = input.Pergunta;
            Resposta = input.Resposta;
            Posicao = input.Posicao;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Pergunta;
        }

        public void Atualizar(Faq input)
        {
            Pergunta = input.Pergunta;
            Resposta = input.Resposta;
            Posicao = input.Posicao;
            DataAtualizacao = DateTime.Now;
            BitAtivo = input.BitAtivo;

            PartitionKey = Pergunta;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
