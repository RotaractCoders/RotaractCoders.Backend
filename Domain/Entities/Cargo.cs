namespace Domain.Entities
{
    public class Cargo
    {
        public string Nome { get; private set; }
        public string TipoCargo { get; private set; }
        public int GestaoDe { get; private set; }
        public int GestaoAte { get; private set; }

        public Cargo(string nome, string tipoCargo, int gestaoDe, int gestaoAte)
        {
            Nome = nome;
            TipoCargo = tipoCargo;
            GestaoDe = gestaoDe;
            GestaoAte = gestaoAte;
        }
    }
}
