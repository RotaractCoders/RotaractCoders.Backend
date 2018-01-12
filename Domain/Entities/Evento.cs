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
        public DateTime DataCriacao { get; set; }
        public DateTime DataEvento { get; set; }
        
        public Evento()
        {

        }

        public Evento(IncluirEventoInput input)
        {
            PartitionKey = input.Nome;
            RowKey = Guid.NewGuid().ToString();

            Nome = input.Nome;
            Realizador = input.Realizador;
            TipoEvento = input.TipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.Data;
            DataCriacao = DateTime.Now;
            Programa = input.Programa;
        }

        public void Atualizar(AtualizarEventoInput input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            TipoEvento = input.TipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
            Programa = input.Programa;
        }

        public void Atualizar(Evento input)
        {
            Nome = input.Nome;
            Realizador = input.Realizador;
            TipoEvento = input.TipoEvento;
            Descricao = input.Descricao;
            DataEvento = input.DataEvento;
            Programa = input.Programa;
        }
    }
}
