using System;
using System.Collections.Generic;

namespace Domain.Commands.Inputs
{
    public class CadastroSocioInput
    {
        public string CodigoSocio { get; set; }
        public string CodigoClube { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Celular { get; set; }
        public string Foto { get; set; }
        public List<CadastrarCargoSocioInput> Cargos { get; set; }
        public List<CadastroSocioClubeInput> Clubes { get; set; }
    }
}
