﻿using Domain.Commands.Inputs;
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
        public DateTime DataAtualizacao { get; set; }

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
            DataAtualizacao = DateTime.Now;

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
            DataAtualizacao = DateTime.Now;

            PartitionKey = input.Nome;
        }
    }
}
