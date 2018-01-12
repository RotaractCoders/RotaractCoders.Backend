using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class Faq : TableEntity
    {
        public int Posicao { get; protected set; }
        public string Pergunta { get; protected set; }
        public string Resposta { get; protected set; }
        public DateTime DataAtualizacao { get; protected set; }

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
            PartitionKey = DataAtualizacao.ToString("dd/MM/yyyy");
        }

        public void Atualizar(string pergunta, string resposta, int posicao)
        {
            Pergunta = pergunta;
            Resposta = resposta;
            Posicao = posicao;
            DataAtualizacao = DateTime.Now;

            PartitionKey = DataAtualizacao.ToString("dd/MM/yyyy");
        }
    }
}
