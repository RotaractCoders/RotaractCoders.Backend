using Domain.Entities;
using System;

namespace Domain.Commands.Results
{
    public class DetalheEventoResult
    {
        public DetalheEventoResult(Evento evento)
        {
            this.Id = evento.RowKey;
            Nome = evento.Nome;
            Realizador = evento.Realizador;
            TipoEvento = evento.TipoEvento;
            Descricao = evento.Descricao;
            DataCriacao = evento.DataCriacao;
            DataEvento = evento.DataEvento;
        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Realizador { get; set; }
        public string TipoEvento { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEvento { get; set; }
    }
}
