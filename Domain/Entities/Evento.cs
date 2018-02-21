using Domain.Commands.Inputs;
using Domain.Entities.Base;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class Evento : TableEntity
    {
        public string Nome { get; set; }
        public string Realizador { get; set; }
        public string TipoEvento { get; set; }
        public string Programa { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEvento { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool BitAtivo { get; set; } = true;

        public Evento()
        {

        }

        public Evento(IncluirEventoInput input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            TipoEvento = input.TipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
            DataCriacao = DateTime.Now;
            Programa = input.Programa;
            Endereco = input.Endereco;
            Latitude = input.Latitude;
            Longitude = input.Longitude;
            DataAtualizacao = DateTime.Now;
            BitAtivo = true;

            PartitionKey = input.Nome;
            RowKey = Guid.NewGuid().ToString();
        }

        public void Atualizar(IncluirEventoInput input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            TipoEvento = input.TipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
            Programa = input.Programa;
            Endereco = input.Endereco;
            Latitude = input.Latitude;
            Longitude = input.Longitude;
            DataAtualizacao = DateTime.Now;

            PartitionKey = input.Nome;
        }

        public void Atualizar(Evento input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            TipoEvento = input.TipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
            Programa = input.Programa;
            Endereco = input.Endereco;
            Latitude = input.Latitude;
            Longitude = input.Longitude;
            DataAtualizacao = DateTime.Now;
            BitAtivo = input.BitAtivo;

            PartitionKey = input.Nome;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
