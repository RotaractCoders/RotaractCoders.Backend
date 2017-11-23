using Domain.Commands.Inputs;
using Domain.Entities.Base;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Socio : Entity
    {
        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Apelido { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string Email { get; private set; }
        public List<SocioClube> SocioClubes { get; private set; }
        public List<CargoDistrito> CargosDistritais { get; private set; }
        public List<CargoRotaractBrasil> CargosRotaractBrasil { get; private set; }
        public List<CargoClube> CargosClube { get; private set; }

        protected Socio()
        {

        }

        public Socio(CadastroSocioInput input)
        {
            AdicionarValidacoes(input);

            Codigo = input.Codigo;
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
        }

        public void Atualizar(CadastroSocioInput input)
        {
            AdicionarValidacoes(input);

            
            Nome = input.Nome;
            Apelido = input.Apelido;
            DataNascimento = input.DataNascimento;
            Email = input.Email;
        }

        private void AdicionarValidacoes(CadastroSocioInput input)
        {
        }
    }
}
