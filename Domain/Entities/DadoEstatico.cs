using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class DadoEstatico : TableEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool BitAtivo { get; set; } = true;

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
        }

        public void AtualizarDescricao(string novaDescricao)
        {
            Descricao = novaDescricao;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Nome;
        }

        public void Atualizar(DadoEstatico dadoEstatico)
        {
            Descricao = dadoEstatico.Descricao;
            BitAtivo = dadoEstatico.BitAtivo;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Nome;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
