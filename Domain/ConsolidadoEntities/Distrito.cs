using Microsoft.WindowsAzure.Storage.Table;

namespace Domain.ConsolidadoEntities
{
    public class Distrito : TableEntity
    {
        public string NumeroDistrito { get; set; }
        public int QuantidadeSocios { get; set; }
        public int QuantidadeClubes { get; set; }

        public Distrito()
        {

        }

        public Distrito(string numeroDistrito, int quantidadeSocios, int quantidadeClubes)
        {
            NumeroDistrito = numeroDistrito;
            QuantidadeClubes = quantidadeClubes;
            QuantidadeSocios = quantidadeSocios;

            RowKey = numeroDistrito;
            PartitionKey = "Brasil";
        }
    }
}
