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

        public void Atualizar(string nome, string categoria, string link)
        {
            Nome = nome;
            Categoria = categoria;
            Link = link;
            DataAtualizacao = DateTime.Now;

            PartitionKey = Categoria;
        }
    }
}
