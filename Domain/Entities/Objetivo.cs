using Domain.Enum;

namespace Domain.Entities
{
    public class Objetivo
    {
        public long Id { get; private set; }
        public TipoObjetivo TipoObjetivo { get; private set; }
        public string Descricao { get; private set; }
    }
}
