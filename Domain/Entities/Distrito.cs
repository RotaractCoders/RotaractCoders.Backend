using Domain.Commands.Inputs;
using Domain.Entities.Base;
using FluentValidator;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Distrito : Entity
    {
        public string Numero { get; private set; }
        public int Regiao { get; private set; }
        public string Mascote { get; private set; }
        public string Site { get; private set; }
        public string Email { get; private set; }
        public List<Clube> Clubes { get; private set; }
        public List<CargoDistrito> CargosDistritais { get; private set; }

        protected Distrito() { }

        public Distrito(CriarDistritoInput input)
        {            
            Numero = input.Numero;
            Regiao = input.Regiao;
            Mascote = input.Mascote;
            Site = input.Site;
            Email = input.Email;
        }

        public void Atualizar(int regiao, string mascote, string site, string email)
        {
            Regiao = regiao;
            Mascote = mascote;
            Site = site;
            Email = email;
        }
    }
}
