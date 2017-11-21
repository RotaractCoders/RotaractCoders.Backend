using System;

namespace Domain.Commands.Inputs
{
    public class AtualizarEventoInput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Realizador { get; set; }
        public Guid IdTipoEvento { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
    }
}
