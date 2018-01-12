using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class DadoEstatico : TableEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public DadoEstatico()
        {

        }

        public DadoEstatico(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            DataAtualizacao = DateTime.Now;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Nome;
            //RowKey = nome;
            //PartitionKey = DataAtualizacao.ToString("dd/MM/yyyy");
        }

        public void AtualizarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Nome;
            //PartitionKey = DataAtualizacao.ToString("dd/MM/yyyy");
        }
    }
}
