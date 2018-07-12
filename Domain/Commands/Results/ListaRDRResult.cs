using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Results
{
    public class ListaRDRResult
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

        public ListaRDRResult(CargoSocio input)
        {
            NomeCargo = input.NomeCargo;
            NomeSocio = input.NomeSocio;
            NomeClube = input.NomeClube;
            CodigoSocio = input.CodigoSocio;
            NumeroDistritoClube = input.NumeroDistritoClube;
            NumeroDistritoCargo = input.NumeroDistritoCargo;
            Foto = input.Foto;
            TipoCargo = input.TipoCargo;
            GestaoDe = input.GestaoDe;
            GestaoAte = input.GestaoAte;
            Programa = input.Programa;
        }
    }
}
