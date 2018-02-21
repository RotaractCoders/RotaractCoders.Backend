using Domain.Commands.Inputs;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Arquivo : TableEntity
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Link { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool BitAtivo { get; set; } = true;

        public Arquivo()
        {

        }

        public Arquivo(string nome, string categoria, string link)
        {
            Nome = nome;
            Categoria = categoria;
            Link = link;
            DataAtualizacao = DateTime.Now;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = Categoria;
        }

        public void Atualizar(IncluirArquivoInput input)
        {
            Nome = input.Nome;
            Categoria = input.Categoria;
            Link = input.Link;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Categoria;
        }

        public void Atualizar(Arquivo input)
        {
            Nome = input.Nome;
            Categoria = input.Categoria;
            Link = input.Link;
            DataAtualizacao = DateTime.Now;
            BitAtivo = input.BitAtivo;

            PartitionKey = Categoria;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
