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

        public Faq(string pergunta, string resposta, int posicao)
        {
            Pergunta = pergunta;
            Resposta = resposta;
            Posicao = posicao;
            DataAtualizacao = DateTime.Now;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Pergunta;
        }

        public void Atualizar(string pergunta, string resposta, int posicao)
        {
            Pergunta = pergunta;
            Resposta = resposta;
            Posicao = posicao;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Pergunta;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
