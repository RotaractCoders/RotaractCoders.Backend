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
        public string Complemento { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
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
            Complemento = input.Complemento;
            Latitude = input.Latitude;
            Longitude = input.Longitude;
            DataAtualizacao = DateTime.Now;
            BitAtivo = true;

            PartitionKey = Guid.NewGuid().ToString();
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
            Complemento = input.Complemento;
            Latitude = input.Latitude;
            Longitude = input.Longitude;
            DataAtualizacao = DateTime.Now;
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
            Complemento = input.Complemento;
            Latitude = input.Latitude;
            Longitude = input.Longitude;
            DataAtualizacao = DateTime.Now;
            BitAtivo = input.BitAtivo;
        }

        public void Inativar()
        {
            BitAtivo = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}
