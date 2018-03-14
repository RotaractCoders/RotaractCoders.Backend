using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Domain.Entities
{
    public class CargoSocio : TableEntity
    {
        public string NomeCargo { get; set; }
        public string NomeSocio { get; set; }
        public string NomeClube { get; set; }
        public string CodigoSocio { get; set; }
        public string NumeroDistritoClube { get; set; }
        public string NumeroDistritoCargo { get; set; }
        public string Foto { get; set; }
        public string TipoCargo { get; set; }
        public DateTime? GestaoDe { get; set; }
        public DateTime? GestaoAte { get; set; }
        public string Programa { get; set; }

        public CargoSocio()
        {

        }

        public CargoSocio(string nomeCargo, string nomeSocio, string nomeClube, string codigoSocio, string numeroDistritoClube, string numeroDistritoCargo, string foto, string tipoCargo, DateTime? gestaoDe, DateTime? gestaoAte, string programa)
        {
            NomeCargo = nomeCargo;
            NomeSocio = nomeSocio;
            NomeClube = nomeClube;
            NumeroDistritoClube = numeroDistritoClube;
            CodigoSocio = codigoSocio;
            NumeroDistritoCargo = numeroDistritoCargo;
            Foto = foto;
            TipoCargo = tipoCargo;
            GestaoDe = gestaoDe;
            GestaoAte = gestaoAte;
            Programa = programa;

            RowKey = Guid.NewGuid().ToString();
            PartitionKey = CodigoSocio;
        }
    }
}
