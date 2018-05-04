using System;

namespace Domain.Commands.Inputs
{
    public class AtualizarEventoInput
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Realizador { get; set; }
        public string Programa { get; set; }
        public string TipoEvento { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
    }
}
