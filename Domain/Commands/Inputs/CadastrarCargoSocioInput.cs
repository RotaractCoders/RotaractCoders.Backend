using System;

namespace Domain.Commands.Inputs
{
    public class CadastrarCargoSocioInput
    {
        public string Nome { get; set; }
        public string TipoCargo { get; set; }

        public DateTime? De { get; set; }
        public DateTime? Ate { get; set; }

        //public int GestaoDe { get; set; }
        //public int GestaoAte { get; set; }
    }
}
