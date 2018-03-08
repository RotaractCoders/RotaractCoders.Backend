using System;

namespace Domain.Entities
{
    public class Cargo
    {
        public string Nome { get; private set; }
        public string TipoCargo { get; private set; }
        public DateTime? GestaoDe { get; private set; }
        public DateTime? GestaoAte { get; private set; }

        public Cargo(string nome, string tipoCargo, DateTime? gestaoDe, DateTime? gestaoAte)
        {
            Nome = nome;
            TipoCargo = tipoCargo;
            GestaoDe = gestaoDe;
            GestaoAte = gestaoAte;
        }
    }
}
